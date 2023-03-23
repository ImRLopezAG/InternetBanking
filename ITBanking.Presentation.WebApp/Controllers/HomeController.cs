﻿using ITBanking.Presentation.WebApp.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.Controllers;
[Authorize]
public class HomeController : Controller
{
  public IActionResult Index() => View();
  
}
