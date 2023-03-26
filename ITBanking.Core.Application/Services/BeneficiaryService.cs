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

public class BeneficiaryService : GenericService<BeneficiaryVm, BeneficiarySaveVm, Beneficiary>, IBeneficiaryService {
  private readonly IBeneficiaryRepository _beneficiaryRepository;
  private readonly IProductRepository _productRepository;
  private readonly IUserService _userService;
  private readonly IMapper _mapper;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse? _currentUser;

  public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IProductRepository productRepository, IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(beneficiaryRepository, mapper) {
    _beneficiaryRepository = beneficiaryRepository;
    _productRepository = productRepository;
    _userService = userService;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }

  public async override Task<IEnumerable<BeneficiaryVm>> GetAll() {
    var users = await _userService.GetAll();
    var products = await _productRepository.GetAll();

    var query = from beneficiary in await _beneficiaryRepository.GetAll()
                where beneficiary.Sender == _currentUser?.Id
                select _mapper.Map<BeneficiaryVm>(beneficiary, opt => opt.AfterMap((src, bnf) => {
                  bnf.Name = users.FirstOrDefault(x => x.Id == bnf.Receptor).FullName;
                  bnf.AccountNumber = products.FirstOrDefault(x => x.Id == bnf.ProductId).AccountNumber;
                }));

    return query;
  }

  public async override Task<BeneficiaryVm> GetById(int id) {
    var users = await _userService.GetAll();
    var products = await _productRepository.GetAll();

    var beneficiary = await _beneficiaryRepository.GetEntity(id);
    var query = _mapper.Map<BeneficiaryVm>(beneficiary, opt => opt.AfterMap((src, bnf) => {
      bnf.Name = users.FirstOrDefault(x => x.Id == bnf.Receptor).FullName;
      bnf.AccountNumber = products.FirstOrDefault(x => x.Id == bnf.ProductId).AccountNumber;
    }));

    return query;
  }
}