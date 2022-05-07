using DAL.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGoogleUsersRepository : IRepository<GoogleUser>
    {
        public Task<GoogleUser> GetByIdAsync(string googleId);
        public Task<GoogleUser> GetByIdAsyncIncludingAsync<Out>(string googleId, Func<GoogleUser, Out> predicate);
        public Task AddAsync(GoogleUser user);
    }
}
