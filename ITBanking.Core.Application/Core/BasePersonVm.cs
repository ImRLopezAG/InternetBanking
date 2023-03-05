
namespace ITBanking.Core.Application.Core;

public class BasePersonVm : BaseVm {
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string DNI { get; set; } = null!;
}
