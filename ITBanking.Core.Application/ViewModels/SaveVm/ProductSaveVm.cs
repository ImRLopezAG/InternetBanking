using System.ComponentModel.DataAnnotations;
using ITBanking.Core.Application.Dtos.Account;

namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class ProductSaveVm : BaseProductVm {
  public string AccountNumber { get; set; } = null!;
  public bool IsPrincipal { get; set; }
  [Required(ErrorMessage = "The User is required")]
  public string UserId { get; set; } = null!;
  [Required(ErrorMessage = "The Account Type is required")]
  public int TyAccountId { get; set; }

  // Ignore mapping
  public IEnumerable<AccountDto>? Users { get; set; } = null!;
}
