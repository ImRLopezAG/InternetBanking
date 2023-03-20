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

  public IActionResult Index()=>View(new TransferVm());

  [HttpPost]
  public async Task<IActionResult> Index(string accountNumber)
  {
        var transfer = await _productService.GetAccount(accountNumber);
        if(transfer == null)
        {
          return View("Index", new TransferVm(){
            HasError = true,
            Error = "Account not found"
          });
          
        }
        return View("Transfer",transfer);
  }


  [HttpPost]
  public async Task<IActionResult> Transfer(TransferSaveVm model)
  {
    return RedirectToAction("Index");
  }

  
}
