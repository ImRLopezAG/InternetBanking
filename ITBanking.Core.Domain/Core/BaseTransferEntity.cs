using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Domain.Core;

public class BaseTransferEntity : BaseProductEntity{
  public int RProductId { get; set; }
  public int SProductId { get; set; }


  public Product SProduct { get; set; } = null!;
  public Product RProduct { get; set; } = null!;
}
