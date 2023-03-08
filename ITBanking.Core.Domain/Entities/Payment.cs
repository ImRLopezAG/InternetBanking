using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Payment : BaseProductEntity {

  public string ReceptorId { get; set; } = null!;
  public string SenderId { get; set; } = null!;

  // Navigation properties
  public int ProductId { get; set; }

  public Product Product { get; set; } = null!;


}
