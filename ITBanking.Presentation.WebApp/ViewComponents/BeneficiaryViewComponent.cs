using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ITBanking.Presentation.WebApp.ViewComponents;

public class BeneficiaryViewComponent : ViewComponent{
  private readonly IBeneficiaryService _beneficiaryService;

  public BeneficiaryViewComponent(IBeneficiaryService beneficiaryService) => _beneficiaryService = beneficiaryService;
  public async Task<IViewComponentResult> InvokeAsync(){ 
    var beneficiaries = await _beneficiaryService.GetAll();
    return View(beneficiaries);
  }
}