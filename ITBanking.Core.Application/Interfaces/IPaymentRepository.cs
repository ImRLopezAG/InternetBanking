using ITBanking.Core.Application.Core;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment> {
  Task DeleteRange(List<Payment> payments);
}
