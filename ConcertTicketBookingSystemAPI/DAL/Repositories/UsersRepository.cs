using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UsersRepository : Repository, IUsersRepository
    {
        public UsersRepository(ApplicationContext context)
            :base(context)
        {
        }

        public Task<User> GetByIdAsync(Guid userId)
        {
            return _context.Users.FirstAsync(u => u.UserId == userId);
        }

        public Task<User> GetByIdIncludingAsync<Out>(Guid userId, Func<User, Out> predicate)
        {
            return _context.Users.Include(u => predicate).FirstAsync(u => u.UserId == userId);
        }

        public IQueryable<User> GetQueryable()
        {
            return _context.Users;
        }

        public Task<bool> IsAdminAsync(Guid userId)
        {
            return _context.Users.AnyAsync(u => u.UserId == userId && u.IsAdmin);
        }

        public Task<bool> IsExistsAsync(Guid userId)
        {
            return _context.Users.AnyAsync(u => u.UserId == userId);
        }
    }
}
