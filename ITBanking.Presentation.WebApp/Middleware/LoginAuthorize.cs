using ITBanking.Presentation.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ITBanking.Presentation.WebApp.Middleware;

public class LoginAuthorize : IAsyncActionFilter {
  private readonly ValidateSessions _userSession;

  public LoginAuthorize(ValidateSessions userSession) {
    _userSession = userSession;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
    if (_userSession.HasUser()) {
      var controller = ( UserController )context.Controller;
      context.Result = controller.RedirectToAction("Index", "Home");
    } else
      await next();
  }
}

