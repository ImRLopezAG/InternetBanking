using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.ViewModels.SaveVm;

namespace ITBanking.Core.Application.Contracts;

public interface IAccountService {
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    Task<string> ConfirmAccountAsync(string userId, string token);
    Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
    Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
    Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
    Task SignOutAsync();
    Task<RegisterResponse> UpdateUserAsync(RegisterRequest request);
    Task<SaveUserVm> GetByIdSave(string id);
    Task<AuthenticationResponse> DeactivateUser(string id);
    Task ActivateUser(string id);
    IEnumerable<AccountDto> GetAll();
    Task<AccountDto> GetById(string id);
}
