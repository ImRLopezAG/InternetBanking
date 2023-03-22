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

public class UserService : IUserService
{
  private readonly IAccountService _accountService;
  private readonly IProductRepository _productRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IEmailService _emailService;
  private readonly IMapper _mapper;

  public UserService(IAccountService accountService,  IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper)
  {
    _accountService = accountService;
    _productRepository = productRepository;
    _httpContextAccessor = httpContextAccessor;
    _emailService = emailService;
    _mapper = mapper;
  }

  public async Task<AuthenticationResponse> LoginAsync(LoginVm model)
  {
    AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(model);
    AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
    return userResponse;
  }
  public async Task SignOutAsync()
  {
    await _accountService.SignOutAsync();
  }

  public async Task<RegisterResponse> RegisterAsync(SaveUserVm model, string origin)
  {
    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(model);
    var registerResponse = await _accountService.RegisterUserAsync(registerRequest, origin);
    if (registerResponse.HasError)
      return registerResponse;
    if (model.Role != 2)
    {
      string pin = Generate.Pin();

      Product productToSave = new(){
        UserId = registerResponse.UserId,
        AccountNumber = pin,
        IsPrincipal = true,
        TyAccountId = 1,
        Amount = (double)model.Amount
      };

      await _productRepository.Save(productToSave);
    }

    return registerResponse;
  }

  public async Task<string> ConfirmEmailAsync(string userId, string token)
  {
    return await _accountService.ConfirmAccountAsync(userId, token);
  }

  public async Task<RegisterResponse> UpdateUserAsync(SaveUserVm vm)
  {
    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
    if(vm.Role ==3){
      var product =await  _productRepository.GetByUser(vm.Id);
      product.Amount += (double) vm.Amount;
      await _productRepository.Update(product);
    }
    return await _accountService.UpdateUserAsync(registerRequest);
  }

  public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordVm model, string origin)
  {
    ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(model);
    return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
  }

  public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordVm model)
  {
    ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(model);
    return await _accountService.ResetPasswordAsync(resetRequest);
  }


  public async Task<IEnumerable<AccountDto>> GetAll(){
    var products = await _productRepository.GetAll();
    var query = from user in await _accountService.GetAll()
                select new AccountDto
                {
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

  public async Task<AccountDto> GetById(string id)=> await _accountService.GetById(id);

  public async Task<SaveUserVm> GetEntity(string id)=> await _accountService.GetEntity(id);


  public async Task ChangeStatus(string id) => await _accountService.ChangeStatus(id);

}
