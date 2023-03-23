using ITBanking.Core.Application.Dtos.Account;
using System.ComponentModel.DataAnnotations;

namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class ProductSaveVm : BaseProductVm {
    public string AccountNumber { get; set; } = null!;
    public bool IsPrincipal { get; set; }
    [Required(ErrorMessage = "The User is required")]
    public string? UserId { get; set; } = null!;
    [Required(ErrorMessage = "The Account Type is required")]
    public int TyAccountId { get; set; }
    public bool? HasLimit { get; set; }
    public double? Limit { get; set; }
    public double? Dbt { get; set; }

    // Ignore mapping
    public IEnumerable<AccountDto>? Users { get; set; } = null!;
}
