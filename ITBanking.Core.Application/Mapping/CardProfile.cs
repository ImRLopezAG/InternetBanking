using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Mapping;

public class CardProfile: Profile
{
    public CardProfile(){
      CreateMap<Card, CardSaveVm>()
        .ReverseMap()
        .ForMember(ent => ent.Product, opt => opt.Ignore());

      CreateMap<Card, CardVm>()
        .ReverseMap()
        .ForMember(ent => ent.Product, opt => opt.Ignore());
    }
}
