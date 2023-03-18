using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class PaymentController: Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public IActionResult Index(){
        return View();
    }

    public async Task<IActionResult> Express ()=> View();
    public async Task<IActionResult> CreditCard ()=> View();
    public async Task<IActionResult> Loans ()=> View();
    public async Task<IActionResult> Beneficiary ()=> View();
}
