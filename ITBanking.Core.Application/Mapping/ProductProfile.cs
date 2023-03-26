using AutoMapper;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Mapping;

public class ProductProfile : Profile {
  public ProductProfile() {

    CreateMap<Product, ProductSaveVm>()
      .ForMember(vm => vm.Users, opt => opt.Ignore())
      .ForMember(vm => vm.HasError, opt => opt.Ignore())
      .ForMember(vm => vm.Error, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.SPayments, opt => opt.Ignore())
      .ForMember(ent => ent.RPayments, opt => opt.Ignore())
      .ForMember(ent => ent.STransfers, opt => opt.Ignore())
      .ForMember(ent => ent.RTransfers, opt => opt.Ignore())
      .ForMember(ent => ent.Beneficiaries, opt => opt.Ignore());

    CreateMap<Product, ProductVm>()
      .ForMember(vm => vm.UserName, opt => opt.Ignore())
      .ForMember(model => model.HasError, opt => opt.Ignore())
      .ForMember(model => model.Error, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(ent => ent.SPayments, opt => opt.Ignore())
      .ForMember(ent => ent.RPayments, opt => opt.Ignore())
      .ForMember(ent => ent.Beneficiaries, opt => opt.Ignore());

    CreateMap<ProductVm, ProductSaveVm>()
      .ForMember(svm => svm.Users, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(vm => vm.UserName, opt => opt.Ignore())
      .ForMember(model => model.Type, opt => opt.Ignore());
  }
}
