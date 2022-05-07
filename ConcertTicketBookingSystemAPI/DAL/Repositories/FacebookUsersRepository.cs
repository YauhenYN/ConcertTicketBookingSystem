using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FacebookUsersRepository : Repository, IFacebookUsersRepository
    {
        public FacebookUsersRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(FacebookUser user)
        {
            await _context.FacebookUsers.AddAsync(user);
        }

        public Task<FacebookUser> GetByIdAsync(string facebookId)
        {
            return _context.FacebookUsers.FirstOrDefaultAsync(u => u.FacebookId == facebookId);
        }

        public Task<FacebookUser> GetByIdAsyncIncludingAsync<Out>(string facebookId, Func<FacebookUser, Out> predicate)
        {
            return _context.FacebookUsers.Include((u) => predicate).FirstOrDefaultAsync(u => u.FacebookId == facebookId);
        }

        public IQueryable<FacebookUser> GetQueryable()
        {
            return _context.FacebookUsers;
        }
    }
}
