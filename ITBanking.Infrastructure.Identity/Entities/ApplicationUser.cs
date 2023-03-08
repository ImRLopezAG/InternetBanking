using Microsoft.AspNetCore.Identity;

namespace ITBanking.Infrastructure.Identity.Entities;
public class ApplicationUser : IdentityUser {
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string DNI { get; set; } = null!;
}

