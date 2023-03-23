using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class AdminListViewComponent: ViewComponent
{
  private readonly IPaymentService _paymentService;
  private readonly ITransferService _transferService;
  private readonly IProductService _productService;
  private readonly IUserService _userService;

  public AdminListViewComponent(IPaymentService paymentService, ITransferService transferService, IProductService productService, IUserService userService){
    _paymentService = paymentService;
    _transferService = transferService;
    _productService = productService;
    _userService = userService;
  }

  public async Task<IViewComponentResult> InvokeAsync(){
    var transfer = await _transferService.GetAll();
    var payment = await _paymentService.GetAll();
    var product = await _productService.GetAll();
    var user = await _userService.GetAll();

    AdminVm admin = new AdminVm(){
      Transfers = transfer.Count(),
      Transfer24Hours = transfer.Where(x => x.CreatedAt >= DateTime.Now.AddDays(-1)).Count(),
      Payments = payment.Count(),
      Payment24Hours = payment.Where(x => x.CreatedAt >= DateTime.Now.AddDays(-1)).Count(),
      Products = product.Count(),
      Clients = user.Where(us => us.Role =="Basic").Count(),
      ActiveClients = user.Where(us => us.Role =="Basic" && us.EmailConfirmed == true).Count(),
      InactiveClients = user.Where(us => us.Role =="Basic" && us.EmailConfirmed == false).Count()
    };
    return View(admin);
  }
}
