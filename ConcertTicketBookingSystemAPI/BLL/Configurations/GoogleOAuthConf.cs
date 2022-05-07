namespace BLL.Configurations
{
    public record GoogleOAuthConf
    {
        public string ServerEndPoint { get; init; }
        public string TokenEndPoint { get; init; }
        public string RefreshEndPoint { get; init; }
        public string Scope { get; init; }
        public string OAuthRedirect { get; init; }
        public string RedirectUrl { get; init; }
    }
}
