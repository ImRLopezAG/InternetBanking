namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class TransferSaveVm : BaseTransferVM {
  public IEnumerable<ProductVm> Products { get; set; } = null!;
  public ProductVm ReceptorModel { get; set; } = null!;
}