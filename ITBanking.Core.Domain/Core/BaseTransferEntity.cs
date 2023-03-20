using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Domain.Core;

public class BaseTransferEntity : BaseProductEntity{
  public int RProductId { get; set; }
  public int SProductId { get; set; }

  public string Sender { get; set; } = null!;
  public string Receptor { get; set; } = null!;

  public Product SProduct { get; set; } = null!;
  public Product RProduct { get; set; } = null!;
}
