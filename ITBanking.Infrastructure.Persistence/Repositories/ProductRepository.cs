using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Domain.Entities;
using ITBanking.Infrastructure.Persistence.Context;
using ITBanking.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ITBanking.Infrastructure.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository {
  private readonly ITBankingContext _context;

  public ProductRepository(ITBankingContext context) : base(context) => _context = context;

  public async Task<Product> GetAccount(string accountNumber) => await _context.Products.FirstOrDefaultAsync(p => p.AccountNumber == accountNumber);
  public async Task<Product> GetByUser(string user) => await _context.Products.Where(pr => pr.IsPrincipal && pr.UserId == user).FirstOrDefaultAsync();
}
