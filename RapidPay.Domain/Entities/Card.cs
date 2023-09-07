namespace RapidPay.Domain.Entities
{
    public class Card : Entity
    {
        public Card(string name, string number, int expirationMonth, int expirationYear, int securityCode, string userId)
        {
            Name = name;
            Number = number;
            ExpirationMonth = expirationMonth;
            ExpirationYear = expirationYear;
            SecurityCode = securityCode;
            UserId = userId;
            Balance = new Balance();
            Payments = new List<Payment>();
        }

        public string Name { get; }
        public string Number { get; }
        public int ExpirationMonth { get; }
        public int ExpirationYear { get; }
        public int SecurityCode { get; }

        public User User { get; set; } = null!;
        public string UserId { get; }

        public Balance Balance { get; }
        public IEnumerable<Payment> Payments { get; }
    }
}
