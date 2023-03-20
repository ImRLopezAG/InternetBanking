using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Application.ViewModels.User;
using ITBanking.Presentation.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;
public class UserController : Controller{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public IActionResult Index()
  {
    return View(new LoginVm());
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  [HttpPost]
  public async Task<IActionResult> Index(LoginVm vm)
  {
    if (!ModelState.IsValid)
    {
      return View(vm);
    }

    AuthenticationResponse userVm = await _userService.LoginAsync(vm);
    if (userVm != null && userVm.HasError != true)
    {
      HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
      return RedirectToRoute(new { controller = "Home", action = "Index" });
    }
    else
    {
      vm.HasError = userVm.HasError;
      vm.Error = userVm.Error;
      return View(vm);
    }
  }
  public async Task<IActionResult> LogOut()
  {
    await _userService.SignOutAsync();
    HttpContext.Session.Remove("user");
    return RedirectToRoute(new { controller = "User", action = "Index" });
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public async Task<IActionResult> ConfirmEmail(string userId, string token)
  {
    string response = await _userService.ConfirmEmailAsync(userId, token);
    return View("ConfirmEmail", response);
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public IActionResult ForgotPassword()
  {
    return View(new ForgotPasswordVm());
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  [HttpPost]
  public async Task<IActionResult> ForgotPassword(ForgotPasswordVm vm)
  {
    if (!ModelState.IsValid)
    {
      return View(vm);
    }
    var origin = Request.Headers["origin"];
    ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
    if (response.HasError)
    {
      vm.HasError = response.HasError;
      vm.Error = response.Error;
      return View(vm);
    }
    return RedirectToRoute(new { controller = "User", action = "Index" });
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  public IActionResult ResetPassword(string token)
  {
    return View(new ResetPasswordVm { Token = token });
  }

  [ServiceFilter(typeof(LoginAuthorize))]
  [HttpPost]
  public async Task<IActionResult> ResetPassword(ResetPasswordVm vm)
  {
    if (!ModelState.IsValid)
    {
      return View(vm);
    }

    ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
    if (response.HasError)
    {
      vm.HasError = response.HasError;
      vm.Error = response.Error;
      return View(vm);
    }
    return RedirectToRoute(new { controller = "User", action = "Index" });
  }

  public IActionResult AccessDenied()
  {
    return View();
  }
}

