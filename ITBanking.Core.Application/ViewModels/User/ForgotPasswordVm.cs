using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITBanking.Core.Application.ViewModels.User;

public class ForgotPasswordVm: ValidationVm
{
  [Required(ErrorMessage = "The email is required")]
  [DataType(DataType.Text)]
  public string Email { get; set; } = null!;
}
