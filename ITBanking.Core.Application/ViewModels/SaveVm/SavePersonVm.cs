using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Core.Models;
using ITBanking.Core.Application.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class SavePersonVm : BaseVm
{
  [Required(ErrorMessage = "Name is required")]
  public string FirstName { get; set; } = null!;
  [Required(ErrorMessage = "Last Name is required")]
  public string LastName { get; set; } = null!;
  [Required(ErrorMessage = "Email is required")]
  public string Email { get; set; } = null!;
  [Required(ErrorMessage = "Phone is required")]
  public string Phone { get; set; } = null!;

  public string DNI { get; set; } = null!;

}
