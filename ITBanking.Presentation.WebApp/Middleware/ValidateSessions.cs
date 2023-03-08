using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels;

namespace ITBanking.Web.Middleware;

public class ValidateSessions {
  private readonly IHttpContextAccessor _httpContextAccessor;

  public ValidateSessions(IHttpContextAccessor httpContextAccessor) {
    _httpContextAccessor = httpContextAccessor;
  }

  public bool HasUser() {
    AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
    return userViewModel != null;
  }
}
