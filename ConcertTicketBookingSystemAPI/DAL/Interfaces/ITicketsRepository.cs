using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITicketsRepository : IRepository<Ticket>
    {
        public Task AddAsync(Ticket ticket);
        public Task<Ticket> GetByIdAsync(Guid id);
    }
}
