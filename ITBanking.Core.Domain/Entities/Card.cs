using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Card : BaseProductEntity{
  public bool? HasLimit { get; set; }
  public double? Limit { get; set; }
  public string Expiration { get; set; } = null!;
  public string Cvv { get; set; } = null!;
  public string Provider { get; set; } = null!;
  public string CardNumber { get; set; } = null!;
  public int TypeId { get; set; }
  public string UserId { get; set; } = null!;

  // Navigation properties
  public int ProductId { get; set; }

  public Product Product { get; set; } = null!;
}
