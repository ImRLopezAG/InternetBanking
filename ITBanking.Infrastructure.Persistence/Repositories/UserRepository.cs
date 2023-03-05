using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Domain.Entities;
using ITBanking.Infrastructure.Persistence.Context;
using ITBanking.Infrastructure.Persistence.Core;

namespace ITBanking.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository {
  private readonly ITBankingContext _context;
  public UserRepository(ITBankingContext context) : base(context) => _context = context;

}