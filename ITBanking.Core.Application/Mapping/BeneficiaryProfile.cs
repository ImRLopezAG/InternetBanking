using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Mapping;

public class BeneficiaryProfile: Profile
{
  public BeneficiaryProfile(){
    CreateMap<Beneficiary, BeneficiarySaveVm>()
      .ReverseMap()
      .ForMember(ent => ent.Product, opt => opt.Ignore());

    CreateMap<Beneficiary, BeneficiaryVm>()
      .ReverseMap()
      .ForMember(ent => ent.Product, opt => opt.Ignore());
  }
}
