using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;
[Authorize(Roles = "Basic")]
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
    _currentUser = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
  }
  [Authorize]
  public IActionResult Index() {
    return View();
  }

[Authorize(Roles = "Basic")]
  public async Task<IActionResult> Express() {
    var items = await _productService.GetAll().ContinueWith(t => t.Result.Where(pd => pd.UserId == _currentUser?.Id && pd.TyAccountId == 1));

    return View(new TransferSaveVm() {
      HasError = false,
      Products = items,
    });
  }
[Authorize(Roles = "Basic")]
  [HttpPost]
  public async Task<IActionResult> Express(TransferSaveVm model) {
    var products = await _productService.GetAccount(model.Receptor);

    if (products == null)
      return View(await TError(model, "Account not found"));

    if (model.SProductId == model.RProductId)
      return View(await TError(model, "You can't transfer to the same account"));

    if (model.SProductId == 0)
      return View(await TError(model, "You must select a account"));

    if (model.Amount <= 0)
      return View(await TError(model, "Amount must be greater than 0"));

    var receptor = await _productService.GetAccount(model.Receptor);
    var sender = await _productService.GetEntity(model.SProductId);


    if (sender.Amount < model.Amount + (model.Amount * 0.0625))
      return View(await TError(model, "Insufficient funds"));

    model.Sender = sender.UserId;
    model.Receptor = receptor.UserId;
    model.RProductId = receptor.Id;


    var result = await _transferService.Save(model);

    if (result.HasError)
      return View(await TError(model, result.Error));

    return RedirectToRoute(new { controller = "Transfer", action = "Index" });
  }
  [Authorize(Roles = "Basic")]
  public async Task<IActionResult> CreditCard() {
    Func<int, IEnumerable<ProductVm>> products = await GetProducts();
    return View(new PaymentSaveVm() {
      HasError = false,
      Saving = products(1),
      Credit = products(2),
    });
  }
  [Authorize(Roles = "Basic")]
  [HttpPost]
  public async Task<IActionResult> CreditCard(PaymentSaveVm model) {
    if (ModelResponse(model).HasError)
      return View(await PayError(model, ModelResponse(model).Error));

    var sender = await _productService.GetEntity(model.SProductId);

    if (sender.Amount < model.Amount + (model.Amount * 0.0625))
      return View(await PayError(model, "Insufficient funds"));

    model.Sender = sender.UserId;
    model.Receptor = sender.UserId;

    var result = await _paymentService.Save(model);

    if (result.HasError)
      return View(await PayError(model, result.Error));


    return RedirectToRoute(new { controller = "Payment", action = "Index" });
  }
  [Authorize(Roles = "Basic")]
  public async Task<IActionResult> Loans() {
    Func<int, IEnumerable<ProductVm>> products = await GetProducts();

    return View(new PaymentSaveVm() {
      HasError = false,
      Saving = products(1),
      Loans = products(3),
    });

  }
  [Authorize(Roles = "Basic")]
  [HttpPost]
  public async Task<IActionResult> Loans(PaymentSaveVm model) {
    if (ModelResponse(model).HasError)
      return View(await PayError(model, ModelResponse(model).Error));
      

    var sender = await _productService.GetEntity(model.SProductId);
    var receptor = await _productService.GetEntity(model.RProductId);

    if (sender.Amount < model.Amount + (model.Amount * 0.0625))
      return View(await PayError(model, "Insufficient funds"));

    model.Sender = sender.UserId;
    model.Receptor = sender.UserId;

    var result = await _paymentService.Save(model);

    if (result.HasError)
      return View(await PayError(model, result.Error));


    return RedirectToRoute(new { controller = "Payment", action = "Index" });
  }
  [Authorize(Roles = "Basic")]
  public async Task<IActionResult> Beneficiary() {
    Func<int, IEnumerable<ProductVm>> products = await GetProducts();
    return View(new PaymentSaveVm() {
      HasError = false,
      Saving = products(1),
    });
  }
[Authorize(Roles = "Basic")]
  [HttpPost]
  public async Task<IActionResult> Beneficiary(PaymentSaveVm model) {
    if (model.SProductId == 0)
      return View(await PayError(model, "You must select a account"));

    if (model.Amount <= 0)
      return View(await PayError(model, "Amount must be greater than 0"));

    var sender = await _productService.GetEntity(model.SProductId);

    if (sender.Amount < model.Amount + (model.Amount * 0.0625))
      return View(await PayError(model, "Insufficient funds"));

    model.Sender = sender.UserId;
    model.Receptor = sender.UserId;

    var result = await _paymentService.Save(model);

    if (result.HasError)
      return View(await PayError(model, result.Error));

    sender.Amount -= model.Amount + (model.Amount * 0.0625);

    await _productService.Edit(sender);

    return RedirectToRoute(new { controller = "Payment", action = "Index" });
  }
  private async Task<Func<int, IEnumerable<ProductVm>>> GetProducts() {
    var items = await _productService.GetAll();
    IEnumerable<ProductVm> products(int type) => items.Where(pd => pd.UserId == _currentUser?.Id && pd.TyAccountId == type);
    return products;
  }
  private async Task<TransferSaveVm> TError(TransferSaveVm model, string error) {
    model.HasError = true;
    model.Error = error;
    model.Products = await _productService.GetAll().ContinueWith(t => t.Result.Where(pd => pd.UserId == _currentUser?.Id && pd.TyAccountId == 1));
    return model;
  }

  private async Task<PaymentSaveVm> PayError(PaymentSaveVm model, string error) {
    Func<int, IEnumerable<ProductVm>> products = await GetProducts();
    model.HasError = true;
    model.Error = error;
    model.Saving = products(1);
    model.Credit = products(2);
    model.Loans = products(3);
    return model;
  }

  private PaymentSaveVm ModelResponse(PaymentSaveVm model){
    model.HasError = false;
    if (model.SProductId == 0){
      model.HasError = true;
      model.Error = "You must select a account";
    }

    if (model.Amount <= 0){
      model.HasError = true;
      model.Error = "Amount must be greater than 0";
    }

    if (model.RProductId == 0){
      model.HasError = true;
      model.Error = "You must select a account";
    }

    return model;
  }

}
