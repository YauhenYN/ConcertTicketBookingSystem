using DAL.Interfaces;
using DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClassicConcertsRepository : Repository, IClassicConcertsRepository
    {
        public ClassicConcertsRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(ClassicConcert concert)
        {
            await _context.ClassicConcerts.AddAsync(concert);
        }

        public IQueryable<ClassicConcert> GetQueryable()
        {
            return _context.ClassicConcerts;
        }
    }
}
