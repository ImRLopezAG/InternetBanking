using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Dtos;
using ITBanking.Core.Application.Dtos.Account;
using ITBanking.Core.Application.Enums;
using ITBanking.Core.Application.Helpers;
using ITBanking.Infrastructure.Identity.Entities;
using ITBanking.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ITBanking.Infrastructure.Identity.Services;
public class AccountService : IAccountService {
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly IEmailService _emailService;
  private readonly IRequestService _requestService;

  public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IRequestService requestService) {
    _userManager = userManager;
    _signInManager = signInManager;
    _emailService = emailService;
    _requestService = requestService;
  }

  public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request) {
    AuthenticationResponse response = new();

    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null) {
      response.HasError = true;
      response.Error = $"No Accounts registered with {request.Email}";
      return response;
    }

    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
    if (!result.Succeeded) {
      response.HasError = true;
      response.Error = $"Invalid credentials for {request.Email}";
      return response;
    }
    if (!user.EmailConfirmed) {
      response.HasError = true;
      response.Error = $"Account no confirmed for {request.Email}";
      return response;
    }

    response.Id = user.Id;
    response.Email = user.Email;
    response.UserName = user.UserName;

    var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

    response.Roles = rolesList.ToList();
    response.IsVerified = user.EmailConfirmed;

    return response;
  }

  public async Task SignOutAsync() {
    await _signInManager.SignOutAsync();
  }

  public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin) {
    RegisterResponse response = new() {
      HasError = false
    };

    var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
    if (userWithSameUserName != null) {
      response.HasError = true;
      response.Error = $"username '{request.UserName}' is already taken.";
      return response;
    }

    var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
    if (userWithSameEmail != null) {
      response.HasError = true;
      response.Error = $"Email '{request.Email}' is already registered.";
      return response;
    }

    var user = new ApplicationUser {
      Email = request.Email,
      FirstName = request.FirstName,
      LastName = request.LastName,
      UserName = request.UserName,
      DNI = request.DNI,
      PhoneNumber = request.PhoneNumber,  
      EmailConfirmed = true,
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    if (result.Succeeded) {
      await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
      var verificationUri = await _requestService.SendVerificationEmail(user, origin);
      await _emailService.SendEmail(new EmailRequest() {
        To = user.Email,
        Body =EmailRequests.ConfirmEmail(user.FirstName, user.LastName, verificationUri),
        Subject = "Confirm registration"
      });
      
      response.UserId = user.Id;

    } else {
      response.HasError = true;
      response.Error = $"An error occurred trying to register the user.";
      return response;
    }
    return response;
  }

  public async Task<string> ConfirmAccountAsync(string userId, string token) {
    var user = await _userManager.FindByIdAsync(userId);
    if (user == null) {
      return $"No accounts registered with this user";
    }

    token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
    var result = await _userManager.ConfirmEmailAsync(user, token);
    if (result.Succeeded) {
      return $"Account confirmed for {user.Email}. You can now use the app";
    } else {
      return $"An error occurred while confirming {user.Email}.";
    }
  }

  public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin) {
    ForgotPasswordResponse response = new() {
      HasError = false
    };

    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user == null) {
      response.HasError = true;
      response.Error = $"No Accounts registered with {request.Email}";
      return response;
    }

    var verificationUri =await _requestService.SendForgotPassword(user, origin);

    await _emailService.SendEmail(new EmailRequest() {
      To = user.Email,
      Body = EmailRequests.ResetPassword(user.UserName, verificationUri),
      Subject = "reset password"
    });


    return response;
  }

  public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request) {
    ResetPasswordResponse response = new() {
      HasError = false
    };

    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user == null) {
      response.HasError = true;
      response.Error = $"No Accounts registered with {request.Email}";
      return response;
    }

    request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
    var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

    if (!result.Succeeded) {
      response.HasError = true;
      response.Error = $"An error occurred while reset password";
      return response;
    }

    return response;
  }

  public async Task<string> GetName (string id){
    var user = await _userManager.FindByIdAsync(id);
    return user.FirstName + " " + user.LastName;
  }

  public IEnumerable<AccountDto> GetAll()
  {
    var accounts = _userManager.Users.AsEnumerable();

    var query = accounts.Select(x => new AccountDto
    {
      Id = x.Id,
      Email = x.Email,
      UserName = x.UserName,
      FullName = x.FirstName + " " + x.LastName,
      DNI = x.DNI,
      EmailConfirmed = x.EmailConfirmed,
      Role = _userManager.GetRolesAsync(x).Result.FirstOrDefault()
    });

    return query;
  }

  public async Task<AccountDto> GetById(string id)
  {
    var account = await _userManager.FindByIdAsync(id);

    var query = new AccountDto
    {
      Id = account.Id,
      Email = account.Email,
      UserName = account.UserName,
      FullName = account.FirstName + " " + account.LastName,
      DNI = account.DNI,
      EmailConfirmed = account.EmailConfirmed,
    };

    return query;
  }
}




