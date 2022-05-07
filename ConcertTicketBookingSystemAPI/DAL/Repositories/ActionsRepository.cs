using DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ActionsRepository : Repository, IActionsRepository
    {
        public ActionsRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddActionAsync(Guid userId, string description)
        {
            await _context.Actions.AddAsync(new Models.Action()
            {
                Date = DateTime.UtcNow,
                UserId = userId,
                Description = description
            });
            await SaveChangesAsync();
        }

        public async Task AddAsync(Models.Action element)
        {
            await _context.AddAsync(element);
        }

        public IQueryable<Models.Action> GetQueryable()
        {
            return _context.Actions;
        }
    }
}
