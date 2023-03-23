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
    private readonly IAccountService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthenticationResponse? _currentUser;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IAccountService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(productRepository, mapper) {
        _productRepository = productRepository;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        _mapper = mapper;
    }

    public async Task AddAmount(double amount, int Id) {
        var product = await _productRepository.GetEntity(Id);
        product.Amount += amount;
        await _productRepository.Update(product);
    }

    public async Task<ProductVm> GetAccount(string accountNumber) {
        var users = await _userService.GetAll();
        var product = await _productRepository.GetAccount(accountNumber);
        if (product == null)
            return null;

        var query = _mapper.Map<ProductVm>(product, opt => opt.AfterMap((src, prd) => {
            prd.Type = GetEnum.Products(product.TyAccountId);
            prd.UserName = users.FirstOrDefault(u => u.Id == product.UserId)?.FullName;
        }));
        return query;
    }

    public async override Task<IEnumerable<ProductVm>> GetAll() {
        var users = await _userService.GetAll();
        var query = from product in await _productRepository.GetAll()
                    join user in users on product.UserId equals user.Id
                    select _mapper.Map<ProductVm>(product, opt => opt.AfterMap((src, prd) => {
                        prd.Type = GetEnum.Products(product.TyAccountId);
                        prd.UserName = user.UserName;
                    }));

        return query;
    }

    public async override Task<ProductSaveVm> Save(ProductSaveVm vm) {
        try {
            var entity = _mapper.Map<Product>(vm);
            if (vm.TyAccountId == 3) {
                entity.Dbt = (vm.Amount) * (-1);
                entity.Amount = 0;
            }
            await _productRepository.Save(entity);
            return _mapper.Map<ProductSaveVm>(entity);
        } catch (Exception ex) {
            vm.HasError = true;
            vm.Error = ex.Message;
            return vm;
        }
    }
    public async override Task<ProductVm> GetById(int id) {
        var users = await _userService.GetAll();
        var product = await _productRepository.GetEntity(id);
        var query = _mapper.Map<ProductVm>(product, opt => opt.AfterMap((src, prd) => {
            prd.Type = GetEnum.Products(product.TyAccountId);
            prd.UserName = users.FirstOrDefault(u => u.Id == product.UserId)?.FullName;
        }));
        return query;
    }
}
