using ITBanking.Core.Application.Core.Models;


namespace ITBanking.Core.Application.ViewModels;

public class BeneficiaryVm : BaseVm {
  public int ProductId { get; set; }
  public string Sender { get; set; } = null!;
  public string Receptor { get; set; } = null!;

  // Ignore Mapping
  public string Name { get; set; } = null!;

  public string AccountNumber { get; set; } = null!;

}
