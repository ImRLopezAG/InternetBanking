using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class Transfer : BaseProductEntity {
  public string ReceptorID { get; set; } = null!;
  public string SenderID { get; set; } = null!;

}
