using AutoMapper;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Application.ViewModels.User;

namespace ITBanking.Core.Application.Mapping;

public class UserProfile : Profile {
  public UserProfile() {

    CreateMap<AuthenticationRequest, LoginVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ReverseMap();

    CreateMap<RegisterRequest, SaveUserVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ReverseMap();

    CreateMap<ForgotPasswordRequest, ForgotPasswordVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ReverseMap();

    CreateMap<ResetPasswordRequest, ResetPasswordVm>()
        .ForMember(model => model.HasError, opt => opt.Ignore())
        .ForMember(model => model.Error, opt => opt.Ignore())
        .ReverseMap();
  }
}
