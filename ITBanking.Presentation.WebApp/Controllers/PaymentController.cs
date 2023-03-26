using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;
public class PaymentController : Controller {
  private readonly IPaymentService _paymentService;
  private readonly ITransferService _transferService;
  private readonly IBeneficiaryService _beneficiaryService;
  private readonly IProductService _productService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IMapper _mapper;
  private readonly AuthenticationResponse? _currentUser;

  public PaymentController(IPaymentService paymentService, ITransferService transferService, IBeneficiaryService beneficiaryService, IProductService productService, IHttpContextAccessor httpContextAccessor, IMapper mapper) {
    _paymentService = paymentService;
    _transferService = transferService;
    _beneficiaryService = beneficiaryService;
    _productService = productService;
    _httpContextAccessor = httpContextAccessor;
    _mapper = mapper;
    _currentUser = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
  }
  [Authorize]
  public IActionResult Index() => View();

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
      Credit = products(2).Where(c => c.Dbt < 0),
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

    var result = await _paymentService.PayCreditCard(model);

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
      Loans = products(3).Where(l => l.Dbt < 0),
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
  public async Task<IActionResult> CashAdvance() {
    Func<int, IEnumerable<ProductVm>> products = await GetProducts();

    return View(new PaymentSaveVm() {
      HasError = false,
      Saving = products(1),
      Credit = products(2),
    });
  }

  [Authorize(Roles = "Basic")]
  [HttpPost]
  public async Task<IActionResult> CashAdvance(PaymentSaveVm model) {
    if (ModelResponse(model).HasError)
      return View(await PayError(model, ModelResponse(model).Error));

    var sender = await _productService.GetEntity(model.SProductId);
    var receptor = await _productService.GetEntity(model.RProductId);

    if (sender.Limit < model.Amount)
      return View(await PayError(model, "You can't exceed the limit"));

    model.Sender = sender.UserId;
    model.Receptor = sender.UserId;

    var result = await _paymentService.SaveAdvance(model);

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
      Beneficiaries = await _beneficiaryService.GetAll()
    });
  }

  [Authorize(Roles = "Basic")]
  [HttpPost]
  public async Task<IActionResult> Beneficiary(PaymentSaveVm model) {
    if (model.SProductId == model.RProductId)
      return View(await PayError(model, "You can't transfer to the same account"));

    if (model.SProductId == 0)
      return View(await PayError(model, "You must select a account"));

    if (string.IsNullOrEmpty(model.Receptor))
      return View(await PayError(model, "You must select a receptor account"));

    if (model.Amount <= 0)
      return View(await PayError(model, "Amount must be greater than 0"));

    var receptor = await _productService.GetAccount(model.Receptor);
    var sender = await _productService.GetEntity(model.SProductId);


    if (sender.Amount < model.Amount + (model.Amount * 0.0625))
      return View(await PayError(model, "Insufficient funds"));

    model.Sender = sender.UserId;
    model.Receptor = receptor.UserId;
    model.RProductId = receptor.Id;

    var result = await _transferService.Save(_mapper.Map<TransferSaveVm>(model));

    if (result.HasError)
      return View(await PayError(model, result.Error));

    return RedirectToRoute(new { controller = "Transfer", action = "Index" });
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

  private PaymentSaveVm ModelResponse(PaymentSaveVm model) {
    model.HasError = false;
    if (model.SProductId == 0) {
      model.HasError = true;
      model.Error = "You must select a account";
    }

    if (model.Amount <= 0) {
      model.HasError = true;
      model.Error = "Amount must be greater than 0";
    }

    if (model.RProductId == 0) {
      model.HasError = true;
      model.Error = "You must select a account";
    }

    return model;
  }
}
