using RapidPay.Application.DTOs;

namespace RapidPay.Application.Interfaces;

public interface IPaymentService
{
    Task<bool> AddPaymentAsync(NewPaymentDto newPayment, string userId);
}