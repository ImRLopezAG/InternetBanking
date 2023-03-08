using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Product : BaseProductEntity {
  public string NAccountId { get; set; } = null!;
  public string Type { get; set; } = null!;
  public bool IsPrincipal { get; set; }
  public string UserId { get; set; } = null!;

  // Navigation properties

  public ICollection<Payment> Payments { get; set; } = null!;
  public ICollection<Beneficiary> Beneficiaries { get; set; } = null!;
  public Card? Card { get; set; } = null!;

}
