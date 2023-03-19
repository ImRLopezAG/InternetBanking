using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Mapping;

public class ProductProfile: Profile
{
  public ProductProfile(){

    CreateMap<Product, ProductSaveVm>()
      .ReverseMap()
      .ForMember(ent => ent.SPayments, opt => opt.Ignore())
      .ForMember(ent => ent.RPayments, opt => opt.Ignore())
      .ForMember(ent => ent.Card, opt => opt.Ignore())
      .ForMember(ent => ent.Beneficiaries, opt => opt.Ignore());

    CreateMap<Product, ProductVm>()
      .ForMember(vm => vm.Card, opt => opt.Ignore())
      .ForMember(model => model.HasError,opt => opt.Ignore())
      .ForMember(model => model.Error,opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.SPayments, opt => opt.Ignore())
      .ForMember(ent => ent.RPayments, opt => opt.Ignore())
      .ForMember(ent => ent.Card, opt => opt.Ignore())
      .ForMember(ent => ent.Beneficiaries, opt => opt.Ignore());
  }
}
