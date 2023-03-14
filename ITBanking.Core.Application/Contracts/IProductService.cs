using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts.Core;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Contracts;

public interface IProductService: IGenericService<ProductVm, ProductSaveVm, Product>
{
    
}
