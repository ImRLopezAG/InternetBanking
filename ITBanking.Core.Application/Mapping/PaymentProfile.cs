using AutoMapper;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Mapping;

public class PaymentProfile : Profile {
  public PaymentProfile() {
    CreateMap<Payment, PaymentSaveVm>()
      .ForMember(model => model.HasError, opt => opt.Ignore())
      .ForMember(model => model.Error, opt => opt.Ignore())
      .ForMember(model => model.Saving, opt => opt.Ignore())
      .ForMember(model => model.Credit, opt => opt.Ignore())
      .ForMember(model => model.Loans, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.RProduct, opt => opt.Ignore())
      .ForMember(ent => ent.SProduct, opt => opt.Ignore());

    CreateMap<Payment, PaymentVm>()
      .ForMember(model => model.HasError, opt => opt.Ignore())
      .ForMember(model => model.Error, opt => opt.Ignore())
      .ForMember(model => model.Type, opt => opt.Ignore())
      .ForMember(model => model.Name, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.RProduct, opt => opt.Ignore())
      .ForMember(ent => ent.SProduct, opt => opt.Ignore());

  }
}
