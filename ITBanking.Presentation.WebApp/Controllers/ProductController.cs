using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Presentation.WebApp.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;
public class ProductController : Controller
{
  private readonly IProductService _productService;
  private readonly IUserService _userService;
  private readonly IMapper _mapper;

  public ProductController(IProductService productService, IUserService userService, IMapper mapper)
  {
    _productService = productService;
    _userService = userService;
    _mapper = mapper;
  }
  [Authorize]
  public IActionResult Index() => View(new ProductVm());

  [Authorize(Roles = "Admin, SuperAdmin")]
  public async Task<IActionResult> Create() => View(new ProductSaveVm()
  {
    HasError = false,
    Users = await _userService.GetAll()
  });
  
  [Authorize(Roles = "Admin, SuperAdmin")]
  [ServiceFilter(typeof(SaveAuthorize))]
  [HttpPost]
  public async Task<IActionResult> Create(ProductSaveVm model)
  {
    var product = await _productService.GetAll().ContinueWith(t => t.Result.Where(p => p.UserId == model.UserId && p.IsPrincipal).FirstOrDefault());
    model.AccountNumber = Generate.Pin();
    model.Users = await _userService.GetAll().ContinueWith(t => t.Result.Where(u => u.Role != "Admin" || u.Role != "SuperAdmin"));

    if (model.TyAccountId > 3 || model.TyAccountId < 1)
    {
      model.HasError = true;
      model.Error = "Account type not found";
      return View(model);
    }

    if ((model.TyAccountId == 2) && (model.Limit == null || model.Limit == 0))
    {
      model.HasError = true;
      model.Error = "You must set a limit";
      return View(model);
    }
    model.HasLimit = model.Limit > 0;

    if (model.TyAccountId == 3)
    {
      product.Amount += model.Amount;
      await _productService.Edit(_mapper.Map<ProductSaveVm>(product));
    }

    if(model.TyAccountId ==2){
      model.Amount = (double)model.Limit;
      model.Dbt = 0;
    }
    var response = await _productService.Save(model);

    if (response.HasError)
    {
      model.HasError = true;
      model.Error = response.Error;
      return View(model);
    }
    return RedirectToAction("Index");
  }

  [Authorize(Roles = "Admin, SuperAdmin")]
  public async Task<IActionResult> AddAmount(double amount, int Id)
  {
    await _productService.AddAmount(amount, Id);
    return RedirectToRoute(new { Controller = "Product", Action = "Index" });
  }

  [Authorize(Roles = "Admin, SuperAdmin")]
  public async Task<IActionResult> Delete(int id){
    var response = await _productService.GetEntity(id);
    if (response == null)
      return View("Index", new ProductVm() { HasError = true, Error = "Product not found" });

    if (response.IsPrincipal)
      return View("Index", new ProductVm() { HasError = true, Error = "You can't delete a principal account" });

    if (response.TyAccountId != 1 && response.Dbt < 0)
    {
      return View("Index", new ProductVm() { HasError = true, Error = "You can't delete this account because you have a debt" });
    }
    else
    {
      await _productService.Delete(id);
    }

    if (response != null && response.TyAccountId ==1){
      var product = await _productService.GetAll().ContinueWith(t => t.Result.Where(pd => pd.UserId == response.UserId && pd.IsPrincipal).FirstOrDefault());
      if (product != null)
      {
        product.Amount += response.Amount;
        await _productService.Edit(_mapper.Map<ProductSaveVm>(product));
      }
    }

    return RedirectToAction("Index");
  }

}
