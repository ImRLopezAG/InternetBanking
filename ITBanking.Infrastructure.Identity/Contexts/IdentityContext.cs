﻿using ITBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITBanking.Infrastructure.Persistence.Contexts;
public class IdentityContext : IdentityDbContext<ApplicationUser> {
  public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }


  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //FLUENT API
    base.OnModelCreating(modelBuilder);
    modelBuilder.HasDefaultSchema("Idt");

    modelBuilder.Entity<ApplicationUser>(entity => {
      entity.ToTable(name: "Users");
    });

    modelBuilder.Entity<IdentityRole>(entity => {
      entity.ToTable(name: "Roles");
    });

    modelBuilder.Entity<IdentityUserRole<string>>(entity => {
      entity.ToTable(name: "UserRoles");
    });

    modelBuilder.Entity<IdentityUserLogin<string>>(entity => {
      entity.ToTable(name: "UserLogins");
    });

  }

}

