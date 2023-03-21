using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Presentation.WebApp.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class ProductController : Controller {
  private readonly IProductService _productService;
  private readonly IUserService _userService;

  public ProductController(IProductService productService, IUserService userService) {
    _productService = productService;
    _userService = userService;
  }

  public IActionResult Index() => View(new ProductVm());
  public async Task<IActionResult> Create() => View(new ProductSaveVm() {
    HasError = false,
    Users = await _userService.GetAll()
  });

  [ServiceFilter(typeof(SaveAuthorize))]
  [HttpPost]
  public async Task<IActionResult> Create(ProductSaveVm model) {
    model.AccountNumber = Generate.Pin();
    model.Users = await _userService.GetAll().ContinueWith(t => t.Result.Where(u => u.Role != "Admin" || u.Role != "SuperAdmin"));
    model.Error = "null";

    if (!ModelState.IsValid) {
      model.HasError = true;
      model.Error = "Invalid data";
      return View(model);
    }

    var response = await _productService.Save(model);

    if (response.HasError) {
      model.HasError = true;
      model.Error = response.Error;
      return View(model);
    }
    return RedirectToAction("Index");
  }

  public async Task<IActionResult> AddAmount(double amount, int Id) {
    await _productService.AddAmount(amount, Id);
    return RedirectToRoute(new { Controller = "Product", Action = "Index" });
  }



  public async Task<IActionResult> Delete(int id) {
    var response = await _productService.GetEntity(id);
    if (response.IsPrincipal) {
      response.HasError = true;
      response.Error = "You can't delete a principal product";

      return View("Index", response);
    }
    if (response != null)
      await _productService.Delete(id);

    return RedirectToAction("Index");
  }

}
