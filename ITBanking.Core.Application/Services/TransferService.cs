using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Services;

public class TransferService: GenericService<TransferVm, TransferSaveVm, Transfer>, ITransferService
{
  private readonly ITransferRepository _transferRepository;
  private readonly IMapper _mapper;
  private readonly IUserService _userService;

  private readonly IProductRepository _productRepository;

  public TransferService(ITransferRepository transferRepository, IMapper mapper, IUserService userService, IProductRepository productRepository) : base(transferRepository, mapper)
  {
    _transferRepository = transferRepository;
    _mapper = mapper;
    _userService = userService;
    _productRepository = productRepository;
  }


  // public override async Task<IEnumerable<TransferVm>> GetAll()
  // {
  //   var users = _userService.GetAll();
  //   var products = await _productRepository.GetAll();
  //   var query = from Transfer in await _transferRepository.GetAll()
  //                   select _mapper.Map<TransferVm>(Transfer, opt => opt.AfterMap((src, trf) =>
  //                   {
  //                     trf.Name = users.FirstOrDefault(x => x.Id == trf.Receptor).FullName;
  //                     trf.AcountNumber = products.FirstOrDefault(x => x.Id == trf.RProductId).AccountNumber;
  //                   }));
  //   return query.ToList();
  // }

//   public override async Task<TransferVm> GetById(int id)
//   {
//     var users = _userService.GetAll();
//     var products = await _productRepository.GetAll();
//     var transfer = await _transferRepository.GetEntity(id);
//     var query = _mapper.Map<TransferVm>(transfer, opt => opt.AfterMap((src, trf) =>
//     {
//       trf.Name = users.FirstOrDefault(x => x.Id == trf.Receptor).FullName;
//       trf.AcountNumber = products.FirstOrDefault(x => x.Id == trf.RProductId).AccountNumber;
//     }));
//     return query;
//   }

 }
