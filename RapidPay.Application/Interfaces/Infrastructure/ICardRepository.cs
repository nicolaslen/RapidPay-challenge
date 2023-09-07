using RapidPay.Domain.Entities;

namespace RapidPay.Application.Interfaces.Infrastructure;

public interface ICardRepository
{
    Task<bool> AddAsync(Card card);
    Task<decimal> GetBalance(int cardId);
    Task<Card> GetAsync(int cardId);
    Task<bool> CardExistsAndBelongsToUser(int cardId, string userId);
}