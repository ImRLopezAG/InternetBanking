using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Infrastructure.Persistence.Context;
using ITBanking.Infrastructure.Persistence.Core;
using ITBanking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITBanking.Infrastructure.Persistence {
  public static class ServiceRegistration {
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration) {
      #region DbContext
      if (configuration.GetValue<bool>("UseInMemoryDatabase")) {
        services.AddDbContext<ITBankingContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
      } else {
        services.AddDbContext<ITBankingContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        m => m.MigrationsAssembly(typeof(ITBankingContext).Assembly.FullName)));
      }
      #endregion
      #region Repositories
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<IPaymentRepository, PaymentRepository>();
      services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
      services.AddScoped<ICardRepository, CardRepository>();
      services.AddScoped<IProductRepository, ProductRepository>();
      #endregion
    }
  }
}