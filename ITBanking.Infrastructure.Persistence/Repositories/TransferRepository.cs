using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Domain.Entities;
using ITBanking.Infrastructure.Persistence.Context;
using ITBanking.Infrastructure.Persistence.Core;

namespace ITBanking.Infrastructure.Persistence.Repositories;

public class TransferRepository : GenericRepository<Transfer>, ITransferRepository {
  private readonly ITBankingContext _context;

  public TransferRepository(ITBankingContext context) : base(context) => _context = context;

}
