using FluentValidation;
using Microsoft.AspNetCore.Identity;
using RapidPay.Domain.Entities;

namespace RapidPay.Application.Services.Cards.Validators;

public class UserValidator : AbstractValidator<string>
{
    private readonly UserManager<User> _userManager;
    public UserValidator(UserManager<User> userManager)
    {
        _userManager = userManager;

        RuleFor(x => x)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(60).WithMessage("{PropertyName} must not exceed 60 characters.")
            .MustAsync(UserExists).WithMessage("The user does not exist.");
    }

    private async Task<bool> UserExists(string userId, CancellationToken token)
    {
        return await _userManager.FindByIdAsync(userId) is not null;
    }
}