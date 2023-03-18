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

  public TransferController(IPaymentService paymentService)
  {
    _paymentService = paymentService;
  }

  public IActionResult Index()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Transfer(PaymentSaveVm model)
  {
    return RedirectToAction("Index");
  }

  
}
