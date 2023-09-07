using FluentValidation;
using Microsoft.AspNetCore.Identity;
using RapidPay.Application.DTOs;

namespace RapidPay.Application.Services.Cards.Validators;

public class AddCardValidator : AbstractValidator<NewCardDto>
{
    public AddCardValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(60).WithMessage("{PropertyName} must not exceed 60 characters.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .Length(15).WithMessage("{PropertyName} must have 15 characters.");

        RuleFor(x => x.ExpirationMonth)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .InclusiveBetween(1, 12).WithMessage("{PropertyName} is invalid.");

        RuleFor(x => x.ExpirationYear)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.Today.Year).WithMessage("{PropertyName} is invalid.");

        RuleFor(x => x)
            .Must(ValidationDateIsValid).WithMessage("Expiration Date is invalid.");

        RuleFor(x => x.SecurityCode)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .InclusiveBetween(1, 9999).WithMessage("{PropertyName} is invalid.");
    }

    private static bool ValidationDateIsValid(NewCardDto cardDto)
    {
        if (!DateTime.TryParse($"01/{cardDto.ExpirationMonth}/{cardDto.ExpirationYear}", out DateTime expirationDate))
            return false;

        return expirationDate >= DateTime.Today;
    }
}