using FluentValidation;
using RapidPay.Application.DTOs;
using RapidPay.Application.Interfaces.Infrastructure;

namespace RapidPay.Application.Services.Payments.Validators;

public class AddPaymentValidator : AbstractValidator<NewPaymentDto>
{
    private readonly ICardRepository _cardRepository;
    private readonly string _userId;
    public AddPaymentValidator(ICardRepository cardRepository, string userId)
    {
        _cardRepository = cardRepository;
        _userId = userId;

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be positive.");

        RuleFor(x => x)
            .MustAsync(CardExistsAndBelongsToUser).WithMessage("The card does not exist.");
    }

    private async Task<bool> CardExistsAndBelongsToUser(NewPaymentDto request, CancellationToken token)
    {
        return await _cardRepository.CardExistsAndBelongsToUser(request.CardId, _userId);
    }
}