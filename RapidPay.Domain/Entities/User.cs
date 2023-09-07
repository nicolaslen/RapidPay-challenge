using Microsoft.AspNetCore.Identity;

namespace RapidPay.Domain.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Cards = new List<Card>();
            Payments = new List<Payment>();
        }
        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
    }
}
