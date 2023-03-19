using ITBanking.Core.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents
{
    public class TransferViewComponent : ViewComponent
    {
        private readonly ITransferService _transfer;

        public TransferViewComponent(ITransferService transfer) => _transfer = transfer;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tansfer = await _transfer.GetAll();
            return View(tansfer);
        }
    }
}
