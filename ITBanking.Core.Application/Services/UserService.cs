using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ITBanking.Core.Application.Services;

public class UserService : GenericService<UserVm, SaveUserVm, User>, IUserService {
  private readonly IUserRepository _userRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IEmailService _emailService;

  private readonly IMapper _mapper;

  public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper) : base(userRepository, mapper) {
    _userRepository = userRepository;
    _httpContextAccessor = httpContextAccessor;
    _emailService = emailService;
    _mapper = mapper;
  }
}
