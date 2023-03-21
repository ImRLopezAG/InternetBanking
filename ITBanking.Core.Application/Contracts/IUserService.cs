using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Application.ViewModels.User;

namespace ITBanking.Core.Application.Contracts;

public interface IUserService {
    Task<string> ConfirmEmailAsync(string userId, string token);
    Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordVm vm, string origin);
    Task<AuthenticationResponse> LoginAsync(LoginVm vm);
    Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin);
    Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordVm vm);
    Task SignOutAsync();
    Task<RegisterResponse> UpdateUserAsync(SaveUserVm vm);
    Task ChangeStatus(string id);
    Task<SaveUserVm> GetEntity(string id);
    Task<IEnumerable<AccountDto>> GetAll();
    Task<AccountDto> GetById(string id);
}

