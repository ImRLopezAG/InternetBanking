using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Domain.Entities;
using ITBanking.Infrastructure.Persistence.Context;
using ITBanking.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ITBanking.Infrastructure.Persistence.Repositories;

public class BeneficiaryRepository : GenericRepository<Beneficiary>, IBeneficiaryRepository {
  private readonly ITBankingContext _context;

  public BeneficiaryRepository(ITBankingContext context) : base(context) => _context = context;

  public async Task<bool> AreBeneficiaries(string sender, string receptor) {
    var result = await _context.Beneficiaries
      .Where(b => b.Sender == sender && b.Receptor == receptor || b.Sender == receptor && b.Receptor == sender)
      .FirstOrDefaultAsync();
    return result != null;
  }
}
