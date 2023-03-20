using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Presentation.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ITBanking.Presentation.WebApp.Middleware;

public class SaveAuthorize : IAsyncActionFilter {
  private readonly ValidateSessions _userSession;
  

  public SaveAuthorize(ValidateSessions userSession) {
    _userSession = userSession;
  } 

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
    if (!_userSession.IsAdmin()) {
      var controller = ( AdminUserController )context.Controller;
      context.Result = controller.RedirectToAction("Index", "Home");
    } else
      await next();
  }
}