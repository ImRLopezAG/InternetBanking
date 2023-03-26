using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents {
  public class TransferViewComponent : ViewComponent {
    private readonly ITransferService _transferService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthenticationResponse? _currentUser;

    public TransferViewComponent(ITransferService transferService, IHttpContextAccessor httpContextAccessor) {
      _transferService = transferService;
      _httpContextAccessor = httpContextAccessor;
      _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
    }

    public async Task<IViewComponentResult> InvokeAsync() {
      var transfer = await _transferService.GetAll();
      if (_currentUser != null && !_currentUser.Roles.Where(x => x.ToString() == "Admin").Any()) {
        transfer = transfer.Where(x => x.Sender == _currentUser.Id);
      }
      return View(transfer);
    }
  }
}
