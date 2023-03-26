using ITBanking.Core.Application.Core.Models;


namespace ITBanking.Core.Application.ViewModels;

public class CardVm : BaseVm {
  public bool? HasLimit { get; set; }
  public double? Limit { get; set; }
  public string Expiration { get; set; } = null!;
  public string Cvv { get; set; } = null!;
  public string Provider { get; set; } = null!;
  public string CardNumber { get; set; } = null!;
  public int TypeId { get; set; }
  public string UserId { get; set; } = null!;
  public int ProductId { get; set; }


  // ignore mapping
  public string Type { get; set; } = null!;
}
