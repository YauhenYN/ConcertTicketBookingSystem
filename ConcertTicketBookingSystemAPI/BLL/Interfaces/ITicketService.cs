using BLL.Dtos.TicketsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITicketService
    {
        public Task<TicketDto> GetTicketByIdAsync(Guid ticketId);
        public Task MarkTicketAsync(Guid ticketId, Guid userId);
        public Task UnmarkTicketAsync(Guid ticketId, Guid userId);
    }
}
