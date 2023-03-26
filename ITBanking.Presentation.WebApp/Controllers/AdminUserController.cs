using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Presentation.WebApp.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;
[Authorize(Roles = "Admin, SuperAdmin")]
public class AdminUserController : Controller {
  private readonly IUserService _userService;

  public AdminUserController(IUserService userService) {
    _userService = userService;
  }

  public IActionResult Index() => View();
  public IActionResult Create() => View(new SaveUserVm());

  [ServiceFilter(typeof(SaveAuthorize))]
  [HttpPost]
  public async Task<IActionResult> Create(SaveUserVm vm) {
    vm.Amount = vm.Amount != null ? vm.Amount : 0;

    if (!ModelState.IsValid)
      return View(vm);


    var origin = Request.Headers["origin"];
    RegisterResponse response = await _userService.RegisterAsync(vm, origin);
    if (response.HasError) {
      vm.HasError = response.HasError;
      vm.Error = response.Error;
      return View(vm);
    }
    return RedirectToRoute(new { controller = "AdminUser", action = "Index" });
  }
  public async Task<IActionResult> ChangeStatus(string id) {
    var userIsVerify = await _userService.GetById(id);

    await _userService.ChangeStatus(userIsVerify.Id);

    return View("Index");
  }

  public async Task<IActionResult> Edit(string id) => View(await _userService.GetEntity(id));

  [ServiceFilter(typeof(SaveAuthorize))]
  [HttpPost]
  public async Task<IActionResult> Edit(SaveUserVm vm) {
    var model = await _userService.GetEntity(vm.Id);
    vm.Role = model.Role;
    vm.Amount = vm.Amount != null ? vm.Amount : 0;

    if (!ModelState.IsValid)
      return View(vm);

    var response = await _userService.UpdateUserAsync(vm);
    if (response.HasError) {
      vm.HasError = response.HasError;
      vm.Error = response.Error;
      return View(vm);
    }
    return RedirectToRoute(new { controller = "AdminUser", action = "Index" });
  }
}
