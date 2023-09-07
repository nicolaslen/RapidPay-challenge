using RapidPay.Application.Interfaces.Infrastructure;
using RapidPay.Domain.Entities;

namespace RapidPay.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly RapidPayDbContext _context;

    public PaymentRepository(RapidPayDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(Payment payment)
    {
        _context.Add(payment);
        return await _context.SaveChangesAsync() > 0;
    }
}