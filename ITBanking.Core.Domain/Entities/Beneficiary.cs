using ITBanking.Core.Domain.Core;
namespace ITBanking.Core.Domain.Entities;
public class Beneficiary : BaseEntity{
  public int ProductId { get; set; }
  public string Sender { get; set; }=null!;
  public string Receptor { get; set; }=null!;

  // Navigation properties
  public Product Product { get; set; } = null!;
}
