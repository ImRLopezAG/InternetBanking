using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Services;

public class ProductService: GenericService<ProductVm, ProductSaveVm,Product>, IProductService
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public ProductService(IProductRepository productRepository, IMapper mapper) :base (productRepository, mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }
}
