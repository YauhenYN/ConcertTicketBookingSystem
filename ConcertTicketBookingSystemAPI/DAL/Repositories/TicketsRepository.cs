using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TicketsRepository : Repository, ITicketsRepository
    {
        public TicketsRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public Task<Ticket> GetByIdAsync(Guid ticketId)
        {
            return _context.Tickets.FirstAsync(t => t.TicketId == ticketId);
        }

        public IQueryable<Ticket> GetQueryable()
        {
            return _context.Tickets;
        }
    }
}
