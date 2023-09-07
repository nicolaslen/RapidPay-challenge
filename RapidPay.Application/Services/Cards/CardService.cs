using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using RapidPay.Application.DTOs;
using RapidPay.Application.Exceptions;
using RapidPay.Application.Interfaces;
using RapidPay.Application.Interfaces.Infrastructure;
using RapidPay.Application.Services.Cards.Validators;
using RapidPay.Domain.Entities;

namespace RapidPay.Application.Services.Cards;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly UserManager<User> _userManager;

    public CardService(ICardRepository cardRepository, UserManager<User> userManager)
    {
        _cardRepository = cardRepository;
        _userManager = userManager;
    }
    public async Task<bool> AddCardAsync(NewCardDto newCard, string userId)
    {
        await ValidateUser(userId);

        var validator = new AddCardValidator();
        ValidationResult validationResult = await validator.ValidateAsync(newCard);

        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult);

        var card = new Card(
            newCard.Name,
            newCard.Number,
            newCard.ExpirationMonth,
            newCard.ExpirationYear,
            newCard.SecurityCode,
            userId);

        return await _cardRepository.AddAsync(card);
    }

    public async Task<decimal> GetCardBalanceAsync(int cardId, string userId)
    {
        if (!await _cardRepository.CardExistsAndBelongsToUser(cardId, userId))
        {
            throw new NotFoundException(nameof(Card), cardId);
        }

        decimal balance = await _cardRepository.GetBalance(cardId);
        return balance;
    }

    private async Task ValidateUser(string userId)
    {
        var validator = new UserValidator(_userManager);
        ValidationResult validationResult = await validator.ValidateAsync(userId);

        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult);
    }
}