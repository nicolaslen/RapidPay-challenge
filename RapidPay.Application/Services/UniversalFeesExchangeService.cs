using RapidPay.Application.Interfaces;

namespace RapidPay.Application.Services;

public class UniversalFeesExchangeService : IUniversalFeesExchangeService
{
    private decimal _fee;
    private static readonly Random Random = new();
    public UniversalFeesExchangeService()
    {
        UpdateFee();
        _ = new Timer(_ => UpdateFee(), null, TimeSpan.Zero, TimeSpan.FromHours(1));
    }

    private static decimal GetRandomBetween0And2()
    {
        const decimal minValue = 0.0M;
        const decimal maxValue = 2.0M;

        decimal randomDecimal = (decimal)Random.NextDouble() * (maxValue - minValue) + minValue;
        return randomDecimal;
    }

    private void UpdateFee()
    {
        _fee *= GetRandomBetween0And2();
    }

    public async Task<decimal> GetFee()
    {
        return await Task.FromResult(_fee);
    }
}