using DAL.Interfaces;
using DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PartyConcertsRepository : Repository, IPartyConcertsRepository
    {
        public PartyConcertsRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(PartyConcert concert)
        {
            await _context.PartyConcerts.AddAsync(concert);
        }

        public IQueryable<PartyConcert> GetQueryable()
        {
            return _context.PartyConcerts;
        }
    }
}
