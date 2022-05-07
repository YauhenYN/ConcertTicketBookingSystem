namespace PayPal
{
    public record PayPalSetup
    {
        public string Environment { get; init; }
        public string ClientId { get; init; }
        public string Secret { get; init; }
        public string ReturnUrl { get; init; }
        public string CancelUrl { get; init; }

    }
}
