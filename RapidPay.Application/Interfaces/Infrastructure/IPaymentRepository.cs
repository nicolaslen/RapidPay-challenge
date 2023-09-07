using RapidPay.Domain.Entities;

namespace RapidPay.Application.Interfaces.Infrastructure;

public interface IPaymentRepository
{
    Task<bool> AddAsync(Payment payment);
}