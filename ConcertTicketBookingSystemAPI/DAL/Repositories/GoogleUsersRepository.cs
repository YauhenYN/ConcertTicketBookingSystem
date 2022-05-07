using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GoogleUsersRepository : Repository, IGoogleUsersRepository
    {
        public GoogleUsersRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(GoogleUser user)
        {
            await _context.GoogleUsers.AddAsync(user);
        }

        public Task<GoogleUser> GetByIdAsync(string googleId)
        {
            return _context.GoogleUsers.FirstAsync(u => u.GoogleId == googleId);
        }

        public Task<GoogleUser> GetByIdAsyncIncludingAsync<Out>(string googleId, Func<GoogleUser, Out> predicate)
        {
            return _context.GoogleUsers.Include(u => predicate).FirstOrDefaultAsync(u => u.GoogleId == googleId);
        }

        public IQueryable<GoogleUser> GetQueryable()
        {
            return _context.GoogleUsers;
        }
    }
}
