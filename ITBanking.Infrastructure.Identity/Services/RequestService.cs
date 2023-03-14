using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITBanking.Core.Application.Contracts;
using ITBanking.Infrastructure.Identity.Entities;
using ITBanking.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace ITBanking.Infrastructure.Identity.Services;

public class RequestService: IRequestService
{
  private readonly IEmailService _emailService;
  private readonly UserManager<ApplicationUser> _userManager;

  public RequestService(IEmailService emailService, UserManager<ApplicationUser> userManager)
  {
    _emailService = emailService;
    _userManager = userManager;
  }

  
  public async Task<string> SendVerificationEmail(ApplicationUser user, string origin) {
    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    var route = "User/ConfirmEmail";
    var Uri = new Uri(string.Concat($"{origin}/", route));
    var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
    verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

    return verificationUri;
  }
  public async Task<string> SendForgotPassword(ApplicationUser user, string origin) {
    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    var route = "User/ResetPassword";
    var Uri = new Uri(string.Concat($"{origin}/", route));
    var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

    return verificationUri;
  }
}
