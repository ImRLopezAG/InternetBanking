using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core.Models;


namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class BeneficiarySaveVm : BaseVm{
  public int ProductId { get; set; }
  public string Sender { get; set; } = null!;
  public string Receptor { get; set; } = null!;
}
