using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core.Models;


namespace ITBanking.Core.Application.ViewModels;

public class PaymentVm: BaseVm{
  public int RProductId { get; set; }
  public int SProductId { get; set; }
  public double Amount { get; set; }
}
