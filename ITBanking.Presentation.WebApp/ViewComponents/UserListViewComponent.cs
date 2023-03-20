using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class UserListViewComponent: ViewComponent
{
  private readonly IUserService _userService;

  public UserListViewComponent(IUserService userService)
  {
    _userService = userService;
  }

  public async Task<IViewComponentResult> InvokeAsync()
  {
    var users = _userService.GetAll();
    return View(users);
  }
}
