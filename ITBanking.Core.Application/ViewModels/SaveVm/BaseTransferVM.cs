namespace ITBanking.Core.Application.ViewModels.SaveVm;
public class BaseTransferVM : BaseProductVm{
  public int RProductId { get; set; }
  public int SProductId { get; set; }
  public string? Sender { get; set; } = null!;
  public string? Receptor { get; set; } = null!;
}

