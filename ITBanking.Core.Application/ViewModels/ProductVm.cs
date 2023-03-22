using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core.Models;

namespace ITBanking.Core.Application.ViewModels;

public class ProductVm: BaseProductVm{
    public string AccountNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsPrincipal { get; set; }
    public string UserId { get; set; } = null!;
    public int TyAccountId { get; set; }
    public bool? HasLimit { get; set; }
    public double? Limit { get; set; }

    public string UserName { get; set; } = null!;
}
