using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.ViewModels.SaveVm;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;

public class TransferController : Controller {
  private readonly ITransferService _transferService;
  private readonly IProductService _productService;
  private readonly IUserService _userService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;

  public TransferController(ITransferService transferService, IProductService productService, IUserService userService, IHttpContextAccessor httpContextAccessor) {
    _transferService = transferService;
    _productService = productService;
    _userService = userService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
  }

  private async Task<TransferSaveVm> Error(TransferSaveVm model, string error) {
    model.HasError = true;
    model.Error = error;
    model.Products = await _productService.GetAll().ContinueWith(t => t.Result.Where(pd => pd.UserId == _currentUser.Id && pd.TyAccountId == 1));
    model.ReceptorModel = await _productService.GetAccount(model.Receptor);
    return model;
  }

  public IActionResult Index() => View(new TransferSaveVm() { HasError = false });

  [HttpPost]
  public async Task<IActionResult> Index(string AccountNumber) {
    var transfer = await _productService.GetAccount(AccountNumber);
    if (transfer != null)
      return RedirectToAction("Create", new { receptor = transfer.AccountNumber });

    return View(new TransferSaveVm() { HasError = true, Error = "Account not found" });
  }

  public async Task<IActionResult> Create(string receptor) {
    var product = await _productService.GetAccount(receptor);
    var items = await _productService.GetAll().ContinueWith(t => t.Result.Where(pd => pd.UserId == _currentUser.Id && pd.TyAccountId == 1));

    return View(new TransferSaveVm() {
      HasError = false,
      Products = items,
      ReceptorModel = product
    });
  }

  [HttpPost]
  public async Task<IActionResult> Create(TransferSaveVm model) {
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

    return RedirectToAction("Index");
  }


}
