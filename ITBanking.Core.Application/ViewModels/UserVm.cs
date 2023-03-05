using ITBanking.Core.Application.Core;

namespace ITBanking.Core.Application.ViewModels;

public class UserVm : BasePersonVm {
  public string UserName { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string Role { get; set; } = null!;
  public bool IsConfirmed { get; set; }
}
