using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MicrosoftUsersRepository : Repository, IMicrosoftUsersRepository
    {
        public MicrosoftUsersRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(MicrosoftUser user)
        {
            await _context.MicrosoftUsers.AddAsync(user);
        }

        public Task<MicrosoftUser> GetByIdAsync(string microsoftId)
        {
            return _context.MicrosoftUsers.FirstOrDefaultAsync(u => u.MicrosoftId == microsoftId);
        }

        public Task<MicrosoftUser> GetByIdAsyncIncludingAsync<Out>(string microsoftId, Func<MicrosoftUser, Out> predicate)
        {
            return _context.MicrosoftUsers.Include(u => predicate).FirstOrDefaultAsync(u => u.MicrosoftId == microsoftId);
        }

        public IQueryable<MicrosoftUser> GetQueryable()
        {
            return _context.MicrosoftUsers;
        }
    }
}
