namespace RapidPay.Domain.Entities;

public class Balance
{
    public Balance()
    {
        Amount = 0;
    }
    public decimal Amount { get; private set; }

    public void AddMovement(decimal amount)
    {
        Amount += amount;
    }
}