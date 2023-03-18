using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core.Models;

namespace ITBanking.Core.Application.ViewModels;

public class ProductVm: BaseVm{
    public string AccountNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsPrincipal { get; set; }
    public string UserId { get; set; } = null!;
    public int TyAccountId { get; set; }

    public CardVm Card { get; set; } = null!;
}
