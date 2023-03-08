using System.ComponentModel.DataAnnotations;

namespace ITBanking.Core.Application.ViewModels.User;

public class LoginVm : ValidationVm
{
  [Required(ErrorMessage = "The email is required")]
  [DataType(DataType.Text)]
  public string Email { get; set; } = null!;

  [Required(ErrorMessage = "You must provide a password")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = null!;

}
