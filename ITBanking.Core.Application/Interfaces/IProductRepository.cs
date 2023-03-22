using ITBanking.Core.Application.Core;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Interfaces;

public interface IProductRepository : IGenericRepository<Product> {
    Task<Product> GetAccount(string accountNumber);
    Task<Product> GetByUser(string user);
}
