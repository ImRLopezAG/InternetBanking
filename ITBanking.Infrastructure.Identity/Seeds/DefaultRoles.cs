using ITBanking.Core.Application.Enums;
using ITBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ITBanking.Infrastructure.Identity.Seeds;
public static class DefaultRoles {
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
    await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
    await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
    await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
  }
}

