using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class PaymentViewComponent: ViewComponent
{
  private readonly IPaymentService _paymentService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;

  public PaymentViewComponent(IPaymentService paymentService, IHttpContextAccessor httpContextAccessor){
    _paymentService = paymentService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }
    public async Task<IViewComponentResult> InvokeAsync()
    {
      var payments = await _paymentService.GetAll();
      if (_currentUser != null && !_currentUser.Roles.Where(x => x.ToString() == "Admin").Any()){
        payments = payments.Where(x => x.Sender == _currentUser.Id);
      }
        return View(payments);
    }
}
