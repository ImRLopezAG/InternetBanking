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

public class BeneficiaryService: GenericService<BeneficiaryVm, BeneficiarySaveVm,Beneficiary>, IBeneficiaryService
{
  private readonly IBeneficiaryRepository _beneficiaryRepository;
  private readonly IMapper _mapper;

  public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IMapper mapper) :base (beneficiaryRepository, mapper)
  {
    _beneficiaryRepository = beneficiaryRepository;
    _mapper = mapper;
  }
}