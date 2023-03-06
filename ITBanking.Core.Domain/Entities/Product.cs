using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Product : BaseProductEntity {
  public string NAccountId { get; set; } = null!;
  public string? Card { get; set; }
  public string Type { get; set; } = null!;
  public bool IsPrincipal { get; set; }
  public string UserId { get; set; } = null!;

}
