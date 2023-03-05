using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities;

public class User : BasePersonEntity {
  public string UserName { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string Role { get; set; } = null!;
  public bool IsConfirmed { get; set; }
}
