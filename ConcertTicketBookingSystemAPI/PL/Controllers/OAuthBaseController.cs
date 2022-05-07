namespace PL.Controllers
{
    public abstract class OAuthBaseController : AuthenticationControllerBase
    {
        protected const string _codeVerifierName = "codeVerifier";
    }
}
