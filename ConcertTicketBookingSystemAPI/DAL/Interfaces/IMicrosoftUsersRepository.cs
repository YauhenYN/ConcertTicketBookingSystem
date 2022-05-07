using DAL.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMicrosoftUsersRepository : IRepository<MicrosoftUser>
    {
        public Task<MicrosoftUser> GetByIdAsync(string microsoftId);
        public Task<MicrosoftUser> GetByIdAsyncIncludingAsync<Out>(string microsoftId, Func<MicrosoftUser, Out> predicate);
        public Task AddAsync(MicrosoftUser user);
    }
}
