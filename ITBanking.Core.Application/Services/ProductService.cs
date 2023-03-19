using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ITBanking.Core.Application.Services;

public class ProductService : GenericService<ProductVm, ProductSaveVm, Product>, IProductService {
  private readonly IProductRepository _productRepository;
  private readonly ICardRepository _cardRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;
  private readonly IMapper _mapper;

  public ProductService(IProductRepository productRepository, ICardRepository cardRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(productRepository, mapper) {
    _productRepository = productRepository;
    _cardRepository = cardRepository;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
    _mapper = mapper;
  }

  public async Task<ProductVm> GetAccount(string accountNumber) {
    var product = await _productRepository.GetAccount(accountNumber);
    var query = _mapper.Map<ProductVm>(product);
    return query;
  }

  public async override Task<IEnumerable<ProductVm>> GetAll() {
    var query = from product in await _productRepository.GetAll()
                where product.UserId == _currentUser?.Id
                join card in await _cardRepository.GetAll() on product.Id equals card.ProductId
                select _mapper.Map<ProductVm>(product, opt => opt.AfterMap((src, prd) => {
                  prd.Type = GetEnum.Products(product.TyAccountId);
                  prd.Card = _mapper.Map<CardVm>(card, opt => opt.AfterMap((src, crd) =>
                      crd.Type = GetEnum.Cards(crd.TypeId)
                  ));
                }));

    return query;
  }

  public async override Task<ProductVm> GetById(int id) {
    var product = await _productRepository.GetEntity(id);
    var card = await _cardRepository.GetByProductId(id);

    var query = _mapper.Map<ProductVm>(product, opt => opt.AfterMap((src, prd) => {
      prd.Type = GetEnum.Products(product.TyAccountId);
      prd.Card = _mapper.Map<CardVm>(card, opt => opt.AfterMap((src, crd) =>
          crd.Type = GetEnum.Cards(crd.TypeId)
      ));
    }));
    return query;
  }
}
