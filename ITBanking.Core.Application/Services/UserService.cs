using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.ViewModels.User;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.Enums;

namespace ITBanking.Core.Application.Services;

public class UserService : IUserService{
  private readonly IAccountService _accountService;
  private readonly ICardRepository _cardRepository;
  private readonly IProductRepository _productRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IEmailService _emailService;
  private readonly IMapper _mapper;

  public UserService(IAccountService accountService, ICardRepository cardRepository, IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper)
  {
    _accountService = accountService;
    _cardRepository = cardRepository;
    _productRepository = productRepository;
    _httpContextAccessor = httpContextAccessor;
    _emailService = emailService;
    _mapper = mapper;
  }

  public async Task<AuthenticationResponse> LoginAsync(LoginVm vm){
    AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
    AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
    return userResponse;
  }
  public async Task SignOutAsync(){
    await _accountService.SignOutAsync();
  }

  public async Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin){
    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
    var registerResponse = await _accountService.RegisterBasicUserAsync(registerRequest, origin);
    string pin = Generate.Pin();

    Product productToSave = new(){
      UserId = registerResponse.UserId,
      AccountNumber = pin,
      IsPrincipal = true,
      TyAccountId = 1,      
    };

    var product = await _productRepository.Save(productToSave);

    Card card = _cardRepository.GenCard();
    card.ProductId = product.Id;
    card.HasLimit = false;
    card.TypeId = 1;
    card.UserId = registerResponse.UserId;
    
    await _cardRepository.Save(card);

    return registerResponse;
  }

  public async Task<string> ConfirmEmailAsync(string userId, string token){
    return await _accountService.ConfirmAccountAsync(userId, token);
  }

  public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordVm vm, string origin)
  {
    ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
    return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
  }

  public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordVm vm)
  {
    ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
    return await _accountService.ResetPasswordAsync(resetRequest);
  }

  public Task<string> GetName(string id)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<AccountDto> GetAll()
  {
    var query = _accountService.GetAll();
    return query;
  }

  public Task<AccountDto> GetById(string id)
  {
    var query = _accountService.GetById(id);
    return query;
  }
}
