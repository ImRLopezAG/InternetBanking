using ITBanking.Core.Application.Dtos.Account;

namespace ITBanking.Core.Application.Contracts;

public interface IAccountService {
  Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
  Task<string> ConfirmAccountAsync(string userId, string token);
  Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
  Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
  Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
  Task SignOutAsync();
}
