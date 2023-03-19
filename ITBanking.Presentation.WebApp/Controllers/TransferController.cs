using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBanking.Presentation.WebApp.Controllers;

public class TransferController : Controller
{
  private readonly IPaymentService _paymentService;
  private readonly IProductService _productService;
  public TransferController(IPaymentService paymentService, IProductService productService)
  {
    _paymentService = paymentService;
    _productService = productService;
  }

  public IActionResult Index()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Index(string accountnumber)
  {
        var transfer = await _productService.GetAccount(accountnumber);
        if(transfer == null)
        {
                ModelState.AddModelError("NoAccount","No existe una cuenta con ese número de cuenta");
                return View("Index");
        }
        return View("Transfer",transfer);
  }


  [HttpPost]
  public async Task<IActionResult> Transfer(PaymentSaveVm model)
  {
    return RedirectToAction("Index");
  }

  
}
