using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core.Models;


namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class PaymentSaveVm: BaseTransferVM{

  public IEnumerable<ProductVm>  Saving{ get; set; }=null!;
  public IEnumerable<ProductVm>  Credit{ get; set; }=null!;
  public IEnumerable<ProductVm>  Loans{ get; set; }=null!;
}
