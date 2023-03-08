using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels.User;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ITBanking.Core.Application.Services;

public class UserService : IUserService
{
  private readonly IAccountService _accountService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IEmailService _emailService;
  private readonly IMapper _mapper;

  public UserService(IAccountService accountService, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper)
  {
    _accountService = accountService;
    _httpContextAccessor = httpContextAccessor;
    _emailService = emailService;
    _mapper = mapper;
  }

  public async Task<AuthenticationResponse> LoginAsync(LoginVm vm)
  {
    AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
    AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
    return userResponse;
  }
  public async Task SignOutAsync()
  {
    await _accountService.SignOutAsync();
  }

  public async Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin)
  {
    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
    return await _accountService.RegisterBasicUserAsync(registerRequest, origin);
  }

  public async Task<string> ConfirmEmailAsync(string userId, string token)
  {
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
}
