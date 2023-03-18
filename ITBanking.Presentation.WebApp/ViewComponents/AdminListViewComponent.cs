using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class AdminListViewComponent: ViewComponent
{
  private readonly IPaymentService _paymentService;

  public AdminListViewComponent(IPaymentService paymentService)=> _paymentService = paymentService;

  public async Task<IViewComponentResult> InvokeAsync(){
    var payments = await _paymentService.GetAll();
    return View(payments.OrderByDescending(p => p.CreatedAt));
  }
}
