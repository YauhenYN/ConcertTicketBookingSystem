using DAL.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        public Task<User> GetByIdAsync(Guid userId);
        public Task<User> GetByIdIncludingAsync<Out>(Guid userId, Func<User, Out> predicate);
        public Task<bool> IsExistsAsync(Guid userId);
        public Task<bool> IsAdminAsync(Guid userId);
    }
}
