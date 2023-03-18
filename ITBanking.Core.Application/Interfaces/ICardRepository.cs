using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Interfaces;
public interface ICardRepository : IGenericRepository<Card>{
  Card GenCard();
  Task<Card> GetByProductId(int product);
}
