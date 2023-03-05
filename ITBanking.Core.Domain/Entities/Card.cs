using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Card : BaseProductEntity {
  public bool? HasLimit { get; set; }
}
