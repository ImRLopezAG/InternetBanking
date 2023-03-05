using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Domain.Settings;
using ITBanking.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITBanking.Infrastructure.Shared;
public static class ServiceRegistration {
  public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration) {
    services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
    services.AddTransient<IEmailService, EmailService>();
  }
}