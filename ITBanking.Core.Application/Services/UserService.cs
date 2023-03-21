using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Application.ViewModels.User;
using ITBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ITBanking.Core.Application.Services;

public class UserService : IUserService {
    private readonly IAccountService _accountService;
    private readonly ICardRepository _cardRepository;
    private readonly IProductRepository _productRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public UserService(IAccountService accountService, ICardRepository cardRepository, IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper) {
        _accountService = accountService;
        _cardRepository = cardRepository;
        _productRepository = productRepository;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginVm model) {
        AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(model);
        AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
        return userResponse;
    }
    public async Task SignOutAsync() {
        await _accountService.SignOutAsync();
    }

    public async Task<RegisterResponse> RegisterAsync(SaveUserVm model, string origin) {
        RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(model);
        var registerResponse = await _accountService.RegisterBasicUserAsync(registerRequest, origin);
        if (registerResponse.HasError)
            return registerResponse;
        if (model.Role != 2) {
            string pin = Generate.Pin();

            Product productToSave = new() {
                UserId = registerResponse.UserId,
                AccountNumber = pin,
                IsPrincipal = true,
                TyAccountId = 1,
                Amount = ( double )model.Amount,
            };

            var product = await _productRepository.Save(productToSave);

            Card card = _cardRepository.GenCard();
            card.ProductId = product.Id;
            card.HasLimit = false;
            card.TypeId = 1;
            card.UserId = registerResponse.UserId;

            await _cardRepository.Save(card);
        }

        return registerResponse;
    }

    public async Task<string> ConfirmEmailAsync(string userId, string token) {
        return await _accountService.ConfirmAccountAsync(userId, token);
    }

    public async Task<RegisterResponse> UpdateUserAsync(SaveUserVm vm) {
        RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
        return await _accountService.UpdateUserAsync(registerRequest);
    }

    public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordVm model, string origin) {
        ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(model);
        return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
    }

    public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordVm model) {
        ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(model);
        return await _accountService.ResetPasswordAsync(resetRequest);
    }


    public async Task<IEnumerable<AccountDto>> GetAll() {
        var products = await _productRepository.GetAll();
        var query = from user in _accountService.GetAll()
                    join product in products on user.Id equals product.UserId
                    select new AccountDto {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        FullName = user.FullName,
                        DNI = user.DNI,
                        EmailConfirmed = user.EmailConfirmed,
                        Products = products.Where(x => x.UserId == user.Id).Count(),
                        Role = user.Role
                    };
        return query;
    }

    public Task<AccountDto> GetById(string id) {
        var query = _accountService.GetById(id);
        return query;
    }

    public Task<SaveUserVm> GetByIdSave(string id) {
        var query = _accountService.GetByIdSave(id);
        return query;
    }


    public async Task ActivateUser(string id) => await _accountService.ActivateUser(id);

}
