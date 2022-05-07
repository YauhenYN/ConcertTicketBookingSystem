using DAL.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IFacebookUsersRepository : IRepository<FacebookUser>
    {
        public Task<FacebookUser> GetByIdAsync(string facebookId);
        public Task<FacebookUser> GetByIdAsyncIncludingAsync<Out>(string facebookId, Func<FacebookUser, Out> predicate);
        public Task AddAsync(FacebookUser user);
    }
}
