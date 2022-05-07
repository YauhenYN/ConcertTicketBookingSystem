using System.Threading.Tasks;

namespace OAuth.Interfaces
{
    public interface IOAuthService
    {
        public Task<dynamic> GetUserCredentialsAsync(string accessToken);
    }
}
