using FluentValidation;
using RapidPay.Application.DTOs;

namespace RapidPay.Infrastructure.Authentication.Validators;

public class RegisterUserValidator : AbstractValidator<RegistrationDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MinimumLength(6).WithMessage("{PropertyName} must have at least 6 characters.")
            .MaximumLength(20).WithMessage("{PropertyName} must have at most 20 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .EmailAddress().WithMessage("{PropertyName} is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MinimumLength(8).WithMessage("{PropertyName} must have at least 8 characters.")
            .Custom((x, context) =>
            {
                if (!x.Any(char.IsDigit))
                {
                    context.AddFailure($"{x} must contain a digit.");
                }
                if (!x.Any(char.IsLower))
                {
                    context.AddFailure($"{x} must contain a lowercase character.");
                }
                if (x.Distinct().Count() == 1)
                {
                    context.AddFailure($"{x} must contain at least two different chars.");
                }
            });
    }
}