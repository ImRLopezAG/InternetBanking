using ITBanking.Core.Application.Contracts.Core;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Application.ViewModels.User;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Contracts;

public interface IUserService{
  Task<string> ConfirmEmailAsync(string userId, string token);
  Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordVm vm, string origin);
  Task<AuthenticationResponse> LoginAsync(LoginVm vm);
  Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin);
  Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordVm vm);
  Task SignOutAsync();
}
