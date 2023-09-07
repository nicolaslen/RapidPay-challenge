namespace RapidPay.Application.DTOs
{
    public class NewCardDto
    {
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int SecurityCode { get; set; }
    }
}
