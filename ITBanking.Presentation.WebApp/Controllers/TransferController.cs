using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class TransferController : Controller {
    private readonly IPaymentService _paymentService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthenticationResponse? _currentUser;

    public TransferController(IPaymentService paymentService, IProductService productService, IUserService userService, IHttpContextAccessor accessor) {
        _paymentService = paymentService;
        _productService = productService;
        _userService = userService;
        _httpContextAccessor = accessor;
        _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");


    }

    public IActionResult Index() => View();


    [HttpPost]
    public async Task<IActionResult> Index(string AccountNumber) {
        var transfer = await _productService.GetAccount(AccountNumber);
        if (transfer == null) {
            return View("Index", new TransferVm() {
                HasError = true,
                Error = "Account not found"
            });

        }
        return View("Transfer", transfer);
    }


    [HttpPost]
    public IActionResult Transfer() {
        //var items = await _productService.GetAll();
        //items = items.Where(x => x.UserId == _currentUser.Id);

        //ViewBag.SaveAccounts = items.Where(client =>
        //client.TyAccountId == ( int )AccountType.PersonalSavings).ToList();

        return View();
    }


}
