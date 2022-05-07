namespace BLL.Configurations
{
    public record PayPalConf
    {
        public string Environment { get; init; }
        public int ConnectionTimeout { get; init; }
        public string ThenRedirectTo { get; init; }
        public string UrlAPI { get; init; }
        public string ReturnURL { get; init; }
        public string SuccessURL { get; init; }
        public string CancelURL { get; init; }
    }
}
