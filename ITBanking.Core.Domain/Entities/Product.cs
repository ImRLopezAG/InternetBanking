using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Product : BaseEntity{
    public string NAccountId { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsPrincipal { get; set; }
    public string UserId { get; set; } = null!;
    public int TyAccountId { get; set; }

    // Navigation properties

    public ICollection<Payment> SPayments { get; set; } = null!;
    public ICollection<Payment> RPayments { get; set; } = null!;
    public ICollection<Beneficiary> Beneficiaries { get; set; } = null!;
    public Card? Card { get; set; } = null!;

}
