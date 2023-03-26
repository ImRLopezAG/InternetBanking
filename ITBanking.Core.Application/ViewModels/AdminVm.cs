namespace ITBanking.Core.Application.ViewModels;

public class AdminVm {
  public int Transfers { get; set; }
  public int Transfer24Hours { get; set; }

  public int Payments { get; set; }
  public int Payment24Hours { get; set; }

  public int Products { get; set; }

  public int Clients { get; set; }
  public int ActiveClients { get; set; }
  public int InactiveClients { get; set; }


}
