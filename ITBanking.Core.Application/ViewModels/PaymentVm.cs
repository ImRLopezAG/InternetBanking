namespace ITBanking.Core.Application.ViewModels;

public class PaymentVm : BaseProductVm {
  public int RProductId { get; set; }
  public int SProductId { get; set; }
  public string AccountNumber { get; set; } = null!;
  public string Sender { get; set; } = null!;
  public string Receptor { get; set; } = null!;

  public string Type { get; set; } = null!;
  public string Name { get; set; } = null!;

}
