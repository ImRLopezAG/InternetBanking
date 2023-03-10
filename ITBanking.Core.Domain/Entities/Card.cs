using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Card : BaseProductEntity
{
    public bool? HasLimit { get; set; }
    public double? Limit { get; set; }

    public int ProductId { get; set; }
    public int TyAccountId { get; set; }


    // Navigation properties
    public Product Product { get; set; } = null!;
    public TypeAccount TypeAccount { get; set; } = null!;


}
