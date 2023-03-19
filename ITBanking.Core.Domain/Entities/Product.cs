using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Product : BaseProductEntity{
    public string AccountNumber { get; set; } = null!;
    public bool IsPrincipal { get; set; }
    public string UserId { get; set; } = null!;
    public int TyAccountId { get; set; }

    // Navigation properties

    public ICollection<Payment> SPayments { get; set; } = null!;
    public ICollection<Payment> RPayments { get; set; } = null!;
    public ICollection<Transfer> STransfers { get; set; } = null!;
    public ICollection<Transfer> RTransfers { get; set; } = null!;
    public ICollection<Beneficiary> Beneficiaries { get; set; } = null!;
    public Card? Card { get; set; } = null!;

}
