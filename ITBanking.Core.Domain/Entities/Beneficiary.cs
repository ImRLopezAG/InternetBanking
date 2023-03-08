using ITBanking.Core.Domain.Core;
namespace ITBanking.Core.Domain.Entities;
public class Beneficiary : BaseEntity {
  public int ProductId { get; set; }
  // Navigation properties
  public Product Product { get; set; } = null!;
}
