namespace RapidPay.Application.Interfaces
{
    public interface IUniversalFeesExchangeService
    {
        Task<decimal> GetFee();
    }
}
