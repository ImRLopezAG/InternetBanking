using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Payment : BaseProductEntity
{

    public string ReceptorId { get; set; } = null!;
    public string SenderId { get; set; } = null!;

    // Navigation properties
    public int RProductId { get; set; }
    public int SProductId { get; set; }


    public Product SProduct { get; set; } = null!;
    public Product RProduct { get; set; } = null!;


}
