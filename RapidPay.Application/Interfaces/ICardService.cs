using RapidPay.Application.DTOs;

namespace RapidPay.Application.Interfaces;

public interface ICardService
{
    Task<bool> AddCardAsync(NewCardDto newCard, string userId);
    Task<decimal> GetCardBalanceAsync(int cardId, string userId);
}