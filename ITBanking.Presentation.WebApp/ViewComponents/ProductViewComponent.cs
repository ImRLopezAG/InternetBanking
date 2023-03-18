using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class ProductViewComponent : ViewComponent
{
  private readonly IProductService _productService;

  public ProductViewComponent(IProductService productService) => _productService = productService;
  public async Task<IViewComponentResult> InvokeAsync()
  {
    var products = await _productService.GetAll();
    return View(products.OrderBy(x => x.IsPrincipal));
  }
}