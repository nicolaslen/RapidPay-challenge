namespace RapidPay.Domain.Entities;

public class Payment : Entity
{
    private Payment()
    {
        //Requested by EF Core
    }
    public Payment(Card card, decimal amount, string userId)
    {
        ArgumentNullException.ThrowIfNull(card, nameof(card));

        card.Balance.AddMovement(amount);
        Card = card;
        UserId = userId;
    }
    public decimal Amount { get; set; }
    public Card Card { get; set; }
    public int CardId { get; set; }
    public User User { get; set; } = null!;
    public string UserId { get; }
}