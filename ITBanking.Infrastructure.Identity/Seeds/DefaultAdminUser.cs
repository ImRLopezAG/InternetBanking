using ITBanking.Core.Application.Enums;
using ITBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ITBanking.Infrastructure.Identity.Seeds;
public static class DefaultAdminUser {
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
    ApplicationUser defaultUser = new() {
      UserName = "adminUser",
      Email = "admin@email.com",
      FirstName = "Admin",
      LastName = "Doe",
      EmailConfirmed = true,
      PhoneNumberConfirmed = true,
      DNI = "adminUser"
    };

    if (userManager.Users.All(u => u.Id != defaultUser.Id)) {
      var user = await userManager.FindByEmailAsync(defaultUser.Email);
      if (user == null) {
        await userManager.CreateAsync(defaultUser, "123Pa$$word!");
        await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
      }
    }

  }
}

