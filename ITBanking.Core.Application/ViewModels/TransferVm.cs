namespace ITBanking.Core.Application.ViewModels;

public class TransferVm : BaseProductVm {
  public int RProductId { get; set; }
  public int SProductId { get; set; }

  public string Sender { get; set; } = null!;
  public string Receptor { get; set; } = null!;

  //Ignore Mapping

  public string Name { get; set; } = null!;

  public string AccountNumber { get; set; } = null!;
}
