using AutoMapper;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Mapping;

public class BeneficiaryProfile : Profile {
  public BeneficiaryProfile() {
    CreateMap<Beneficiary, BeneficiarySaveVm>()
      .ForMember(model => model.HasError, opt => opt.Ignore())
      .ForMember(model => model.Error, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.Product, opt => opt.Ignore());

    CreateMap<Beneficiary, BeneficiaryVm>()
      .ForMember(model => model.HasError, opt => opt.Ignore())
      .ForMember(model => model.Error, opt => opt.Ignore())
      .ForMember(model => model.Name, opt => opt.Ignore())
      .ForMember(model => model.AccountNumber, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.Product, opt => opt.Ignore());
  }
}
