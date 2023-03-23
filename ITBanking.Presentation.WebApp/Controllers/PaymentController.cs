using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels.SaveVm;

using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class PaymentController : Controller {
    private readonly IPaymentService _paymentService;
    private readonly ITransferService _transferService;

    private readonly IProductService _productService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthenticationResponse? _currentUser;

    public PaymentController(IPaymentService paymentService, IProductService productService, IHttpContextAccessor httpContextAccessor, ITransferService transferService) {
        _paymentService = paymentService;
        _productService = productService;
        _httpContextAccessor = httpContextAccessor;
        _transferService = transferService;
        _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
    }


    public IActionResult Index() {
        return View();
    }

    private async Task<TransferSaveVm> Error(TransferSaveVm model, string error) {
        model.HasError = true;
        model.Error = error;
        model.Products = await _productService.GetAll().ContinueWith(t => t.Result.Where(pd => pd.UserId == _currentUser.Id && pd.TyAccountId == 1));
        model.ReceptorModel = await _productService.GetAccount(model.Receptor);
        return model;
    }
    public async Task<IActionResult> Express() {
        var items = await _productService.GetAll().ContinueWith(t => t.Result.Where(pd => pd.UserId == _currentUser.Id && pd.TyAccountId == 1));

        return View(new TransferSaveVm() {
            HasError = false,
            Products = items,

        });
    }

    [HttpPost]
    public async Task<IActionResult> Express(TransferSaveVm model) {

        var products = await _productService.GetAccount(model.Receptor);

        model.ReceptorModel = products;

        if (model.SProductId == model.RProductId)
            return View(await Error(model, "You can't transfer to the same account"));

        if (model.SProductId == 0)
            return View(await Error(model, "You must select a account"));

        if (model.RProductId == 0)
            return View(await Error(model, "You must select a receptor account"));

        if (model.Amount <= 0)
            return View(await Error(model, "Amount must be greater than 0"));

        var receptor = await _productService.GetAccount(model.Receptor);
        var sender = await _productService.GetEntity(model.SProductId);


        if (sender.Amount < model.Amount + (model.Amount * 0.0625))
            return View(await Error(model, "Insufficient funds"));

        model.Sender = sender.UserId;
        model.Receptor = receptor.UserId;


        var result = await _transferService.Save(model);

        if (result.HasError)
            return View(await Error(model, result.Error));

        return RedirectToRoute(new { controller = "Transfer", action = "Index" });
    }

    public async Task<IActionResult> CreditCard() => View();
    public async Task<IActionResult> Loans() => View();
    public async Task<IActionResult> Beneficiary() => View();
}
