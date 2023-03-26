using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class ProductViewComponent : ViewComponent {
  private readonly IProductService _productService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;

  public ProductViewComponent(IProductService productService, IHttpContextAccessor httpContextAccessor) {
    _productService = productService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }
  public async Task<IViewComponentResult> InvokeAsync() {
    var products = await _productService.GetAll();
    if (_currentUser != null && !_currentUser.Roles.Where(x => x.ToString() == "Admin").Any()) {
      products = products.Where(x => x.UserId == _currentUser.Id);
    }
    return View(products.OrderByDescending(x => x.IsPrincipal));
  }
}