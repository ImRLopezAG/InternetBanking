using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class BeneficiaryController : Controller{
  private readonly IBeneficiaryService _beneficiaryService;
  private readonly IProductService _productService;
  private readonly IHttpContextAccessor _httpContextAccessor;

  private AuthenticationResponse _currentUser;

  public BeneficiaryController(IBeneficiaryService beneficiaryService, IProductService productService, IHttpContextAccessor httpContextAccessor)
  {
    _beneficiaryService = beneficiaryService;
    _productService = productService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }

  public IActionResult Index()
  {
    return View(new BeneficiaryVm());
  }
  [HttpPost]
  public async Task<IActionResult> Add(string accountNumber)
  {
    try
    {
      var product = await _productService.GetAccount(accountNumber);

      if (product == null || product.TyAccountId != 1 || product.UserId == _currentUser.Id)
      {
        return View("Index", new BeneficiaryVm
        {
          HasError = true,
          Error = "account number not found"
        });
      }
      var beneficiaries = await _beneficiaryService.GetAll().ContinueWith(t => t.Result.Where(w => w.ProductId == product.Id && w.Sender == _currentUser.Id));

      if (beneficiaries.Any()){
        return View("Index", new BeneficiaryVm
        {
          HasError = true,
          Error = "account number already added"
        });
      }
      var beneficiary = new BeneficiarySaveVm
      {
        ProductId = (int)product.Id,
        Sender = _currentUser.Id,
        Receptor = product.UserId,
      };
      await _beneficiaryService.Save(beneficiary);
      return RedirectToAction("Index");

    }
    catch
    {
      return View("Index");
    }
  }

  public async Task<IActionResult> Delete(int id)
  {
    await _beneficiaryService.Delete(id);
    return RedirectToAction("Index");
  }
}
