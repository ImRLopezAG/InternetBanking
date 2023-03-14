using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core.Models;


namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class CardSaveVm: BaseVm
{
  public bool? HasLimit { get; set; }
  public double? Limit { get; set; }
  public string Expiration { get; set; } = null!;
  public string Cvv { get; set; } = null!;
  public string Provider { get; set; } = null!;
  // Navigation properties
  public int ProductId { get; set; }
}
