using DAL.Interfaces;
using DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OpenAirConcertsRepository : Repository, IOpenAirConcertsRepository
    {
        public OpenAirConcertsRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(OpenAirConcert concert)
        {
            await _context.OpenAirConcerts.AddAsync(concert);
        }

        public IQueryable<OpenAirConcert> GetQueryable()
        {
            return _context.OpenAirConcerts;
        }
    }
}
