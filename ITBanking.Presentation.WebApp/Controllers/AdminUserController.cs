using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.ViewModels.SaveVm;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class AdminUserController : Controller {
  private readonly IUserService _userService;

  public AdminUserController(IUserService userService) {
    _userService = userService;
  }

  public IActionResult Index() => View();



  public IActionResult Create() {
    return View(new SaveUserVm());
  }

  [HttpPost]
  public async Task<IActionResult> Create(SaveUserVm vm) {
    vm.Amount = vm.Amount != null ? vm.Amount : 0;

    var origin = Request.Headers["origin"];
    RegisterResponse response = await _userService.RegisterAsync(vm, origin);
    if (response.HasError) {
      vm.HasError = response.HasError;
      vm.Error = response.Error;
      return View(vm);
    }
    return RedirectToRoute(new { controller = "AdminUser", action = "Index" });
  }
}
