using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
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
    public IActionResult Create() => View(new ProductSaveVm());

    [HttpPost]
    public async Task<IActionResult> Create(ProductSaveVm model) {
        if (!ModelState.IsValid)
            return View(model);

        var response = await _productService.Save(model);

        if (response.HasError) {
            ModelState.AddModelError("", response.Error);
            return View(model);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> AddAmount(double amount, int Id) {
        var add = await _productService.GetById(Id);
        var user = await _userService.GetByIdSave(add.UserId);

        user.Amount += amount;

        await _userService.UpdateUserAsync(user);
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
