using ITBanking.Core.Application.Enums;
using ITBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ITBanking.Infrastructure.Identity.Seeds;
public static class DefaultBasicUser {
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
    ApplicationUser defaultUser = new() {
      UserName = "basicUser",
      Email = "basic@email.com",
      FirstName = "Basic",
      LastName = "Doe",
      EmailConfirmed = true,
      PhoneNumberConfirmed = true,
      DNI = "basicUser"
    };

    if (userManager.Users.All(u => u.Id != defaultUser.Id)) {
      var user = await userManager.FindByEmailAsync(defaultUser.Email);
      if (user == null) {
        await userManager.CreateAsync(defaultUser, "123Pa$$word!");
        await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
      }
    }

  }
}

