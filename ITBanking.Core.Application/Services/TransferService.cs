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

  public TransferService(ITransferRepository transferRepository, IMapper mapper) : base(transferRepository, mapper)
  {
    _transferRepository = transferRepository;
    _mapper = mapper;
  }
}
