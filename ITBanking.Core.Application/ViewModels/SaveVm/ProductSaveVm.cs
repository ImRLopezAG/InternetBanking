using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core.Models;


namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class ProductSaveVm : BaseVm{
  public string NAccountId { get; set; } = null!;
  public bool IsPrincipal { get; set; }
  public string UserId { get; set; } = null!;
  public int TyAccountId { get; set; }
}
