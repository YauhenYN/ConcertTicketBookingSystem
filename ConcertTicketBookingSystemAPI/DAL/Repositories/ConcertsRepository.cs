using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ConcertsRepository : Repository, IConcertsRepository
    {
        public ConcertsRepository(ApplicationContext context)
            :base(context)
        {
        }

        public Task<Concert> GetByIdAsync(int id)
        {
            return _context.Concerts.FirstOrDefaultAsync(c => c.ConcertId == id);
        }

        public Task<Concert> GetByIdIncludingAsync<Y>(int id, Func<Concert, Y> predicate)
        {
            return _context.Concerts.Include(c => predicate).FirstOrDefaultAsync(c => c.ConcertId == id);
        }

        public IQueryable<Concert> GetQueryable()
        {
            return _context.Concerts;
        }

        public Task<bool> IsExistsAsync(int id)
        {
            return _context.Concerts.AnyAsync(c => c.ConcertId == id);
        }
    }
}
