using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Contracts.Core;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ITBanking.Core.Application;

public static class ServiceRegistration {
  public static void AddApplicationServices(this IServiceCollection services) {
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    #region Services
    services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
    services.AddTransient<IUserService, UserService>();
    #endregion
  }
}
