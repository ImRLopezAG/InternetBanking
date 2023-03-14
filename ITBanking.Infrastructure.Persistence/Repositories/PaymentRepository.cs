using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Domain.Entities;
using ITBanking.Infrastructure.Persistence.Context;
using ITBanking.Infrastructure.Persistence.Core;

namespace ITBanking.Infrastructure.Persistence.Repositories;

public class PaymentRepository: GenericRepository<Payment>, IPaymentRepository
{
  private readonly ITBankingContext _context;

  public PaymentRepository(ITBankingContext context) : base(context) => _context = context;
  
}
