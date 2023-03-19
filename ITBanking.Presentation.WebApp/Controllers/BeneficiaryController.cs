using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels.SaveVm;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class BeneficiaryController : Controller {
  private readonly IBeneficiaryService _beneficiaryService;
  private readonly IProductService _productService;
  private readonly IHttpContextAccessor _httpContextAccessor;

  private AuthenticationResponse _currentUser;

  public BeneficiaryController(IBeneficiaryService beneficiaryService, IProductService productService, IHttpContextAccessor httpContextAccessor) {
    _beneficiaryService = beneficiaryService;
    _productService = productService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }

  public IActionResult Index() {
    return View();
  }
  [HttpPost]
  public async Task<IActionResult> Add(string accountNumber) {
    try {
      var product = await _productService.GetAccount(accountNumber);
      if (product == null || product.UserId == _currentUser.Id || product.TyAccountId != 1) {
        ModelState.AddModelError("", "accountNumber is invalid or you are the owner");
        return View("Index");
      }

      var beneficiary = new BeneficiarySaveVm {
        ProductId = product.Id,
        Sender = _currentUser.Id,
        Receptor = product.UserId,
      };
      await _beneficiaryService.Save(beneficiary);
      return View("Index");

    } catch {
      return View("Index");
    }
  }

  public async Task<IActionResult> Delete(int id){
    await _beneficiaryService.Delete(id);
    return RedirectToAction("Index");
  }
}