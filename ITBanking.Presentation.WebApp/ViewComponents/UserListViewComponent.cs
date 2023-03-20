using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class UserListViewComponent: ViewComponent
{
  private readonly IUserService _userService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse _currentUser;

  public UserListViewComponent(IUserService userService, IHttpContextAccessor httpContextAccessor)
  {
    _userService = userService;
    _httpContextAccessor = httpContextAccessor;
    _currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }

  public async Task<IViewComponentResult> InvokeAsync()
  {
    var users =await _userService.GetAll().ContinueWith(x => x.Result.Where(us => us.Id != _currentUser.Id));
    return View(users);
  }
}
