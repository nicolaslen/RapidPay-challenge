using FluentValidation.Results;
using RapidPay.Application.DTOs;
using RapidPay.Application.Interfaces;
using RapidPay.Application.Interfaces.Infrastructure;
using RapidPay.Application.Services.Payments.Validators;
using RapidPay.Domain.Entities;

namespace RapidPay.Application.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly ICardRepository _cardRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUniversalFeesExchangeService _universalFeesExchangeService;

    public PaymentService(ICardRepository cardRepository, IPaymentRepository paymentRepository, IUniversalFeesExchangeService universalFeesExchangeService)
    {
        _cardRepository = cardRepository;
        _paymentRepository = paymentRepository;
        _universalFeesExchangeService = universalFeesExchangeService;
    }
    public async Task<bool> AddPaymentAsync(NewPaymentDto newPayment, string userId)
    {
        var validator = new AddPaymentValidator(_cardRepository, userId);
        ValidationResult validationResult = await validator.ValidateAsync(newPayment);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        Card card = await _cardRepository.GetAsync(newPayment.CardId);
        decimal amountWithFee = newPayment.Amount + await _universalFeesExchangeService.GetFee();

        var payment =
            new Payment(
                card,
                amountWithFee,
                userId);

        return await _paymentRepository.AddAsync(payment);
    }
}