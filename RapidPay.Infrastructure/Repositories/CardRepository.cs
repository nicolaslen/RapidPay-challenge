using Microsoft.EntityFrameworkCore;
using RapidPay.Application.Interfaces.Infrastructure;
using RapidPay.Domain.Entities;

namespace RapidPay.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly RapidPayDbContext _context;

    public CardRepository(RapidPayDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddAsync(Card card)
    {
        _context.Add(card);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<decimal> GetBalance(int cardId)
    {
        decimal balance = await _context.Cards
            .Where(x => x.Id == cardId)
            .Select(x => x.Balance.Amount)
            .SingleAsync();

        return balance;
    }

    public async Task<Card> GetAsync(int cardId)
    {
        return await _context.Cards.FindAsync(cardId);
    }

    public async Task<bool> CardExistsAndBelongsToUser(int cardId, string userId)
    {
        return await _context.Cards.AnyAsync(x => x.Id == cardId && x.UserId == userId);
    }
}