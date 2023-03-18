using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Domain.Entities;
using ITBanking.Infrastructure.Persistence.Context;
using ITBanking.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ITBanking.Infrastructure.Persistence.Repositories;

public class CardRepository : GenericRepository<Card>, ICardRepository {
  private readonly ITBankingContext _context;

  public CardRepository(ITBankingContext context) : base(context) => _context = context;

  public Card GenCard() {
    Card card = new() {
      CardNumber = Generate.CardNumber(),
      Cvv = Generate.CardCvv(),
      Expiration = Generate.CardExpiryDate(),
    };
    card.Provider = Generate.CardProvider(card.CardNumber);

    return card;
  }

  public Task<Card> GetByProductId(int product) => _context.Cards.FirstOrDefaultAsync(c => c.Id == product);
}
