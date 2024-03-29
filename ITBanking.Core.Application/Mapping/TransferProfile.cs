﻿using AutoMapper;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Mapping;
public class TransferProfile : Profile {
  public TransferProfile() {
    CreateMap<Transfer, TransferVm>()
      .ForMember(model => model.HasError, opt => opt.Ignore())
      .ForMember(model => model.Error, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.RProduct, opt => opt.Ignore())
      .ForMember(ent => ent.SProduct, opt => opt.Ignore());

    CreateMap<Transfer, TransferSaveVm>()
      .ForMember(model => model.HasError, opt => opt.Ignore())
      .ForMember(model => model.Error, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.RProduct, opt => opt.Ignore())
      .ForMember(ent => ent.SProduct, opt => opt.Ignore());

    CreateMap<TransferSaveVm, PaymentSaveVm>()
      .ForMember(pay => pay.Beneficiaries, opt => opt.Ignore())
      .ForMember(pay => pay.Saving, opt => opt.Ignore())
      .ForMember(pay => pay.Credit, opt => opt.Ignore())
      .ForMember(pay => pay.Loans, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(trf => trf.Products, opt => opt.Ignore())
      .ForMember(trf => trf.ReceptorModel, opt => opt.Ignore());
  }
}
