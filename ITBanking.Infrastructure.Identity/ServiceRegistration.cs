using ITBanking.Core.Application.Contracts;
using ITBanking.Infrastructure.Identity.Entities;
using ITBanking.Infrastructure.Identity.Interfaces;
using ITBanking.Infrastructure.Identity.Services;
using ITBanking.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITBanking.Infrastructure.Identity;

public static class ServiceRegistration {
  public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration) {
    #region Contexts
    if (configuration.GetValue<bool>("UseInMemoryDatabase")) {
      services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
    } else {
      services.AddDbContext<IdentityContext>(options => {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
      });
    }
    #endregion

    #region Identity
    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

    services.ConfigureApplicationCookie(options => {
      options.LoginPath = "/User";
      options.AccessDeniedPath = "/User/AccessDenied";
    });

    services.AddAuthentication();
    #endregion

    #region Services
    services.AddTransient<IAccountService, AccountService>();
    services.AddTransient<IRequestService, RequestService>();
    #endregion
  }
}

