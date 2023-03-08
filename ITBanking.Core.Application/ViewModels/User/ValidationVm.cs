using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core;

namespace ITBanking.Core.Application.ViewModels.User;

public class ValidationVm
{
  public bool HasError { get; set; }
  public string? Error { get; set; }
}
