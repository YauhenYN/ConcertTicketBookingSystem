using BLL.Dtos.TicketsDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TicketsService : ICommonTicketsService
    {

        private readonly ITicketsRepository _ticketsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IActionsRepository _actionsRepository;

        public TicketsService(ITicketsRepository ticketsRepository,
            IUsersRepository usersRepository,
            IActionsRepository actionsRepository)
        {
            _ticketsRepository = ticketsRepository;
            _usersRepository = usersRepository;
            _actionsRepository = actionsRepository;
        }

        public async Task<TicketSelectorDto> GetManyTicketsAsync(TicketSelectParametersDto dto, Guid userId)
        {
            IQueryable<Ticket> tickets = _ticketsRepository.GetQueryable();
            tickets = tickets.Include(t => t.Concert);
            if (dto.ByUserId != null) tickets = tickets.Where(t => dto.ByUserId == t.UserId);
            if (dto.ByConcertId != null) tickets = tickets.Where(t => t.ConcertId == dto.ByConcertId);
            if (dto.ByTicketId != null && await _usersRepository.IsAdminAsync(userId))
            {
                tickets = tickets.Where(t => t.TicketId.ToString().ToLower().Contains(dto.ByTicketId.ToLower()));
            }
            var ticketsCount = tickets.Count();
            TicketSelectorDto selectorDto = new TicketSelectorDto()
            {
                PageCount = (ticketsCount / dto.NeededCount) + 1,
                CurrentPage = dto.PageNumber,
                Tickets = await tickets.Skip(dto.PageNumber * dto.NeededCount).Take(dto.NeededCount).ToDtosAsync()
            };
            return selectorDto;
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId)
        {
            var ticket = await _ticketsRepository.GetByIdAsync(ticketId);
            return ticket == null ? null : ticket.ToDto();
        }

        public async Task MarkTicketAsync(Guid ticketId, Guid userId)
        {
            var ticket = await _ticketsRepository.GetByIdAsync(ticketId);
            ticket.IsMarkedFlag = true;
            await _actionsRepository.AddActionAsync(userId, "Marked Ticket = " + ticketId);
            await _actionsRepository.SaveChangesAsync();
        }

        public async Task UnmarkTicketAsync(Guid ticketId, Guid userId)
        {
            var ticket = await _ticketsRepository.GetByIdAsync(ticketId);
            ticket.IsMarkedFlag = false;
            await _actionsRepository.AddActionAsync(userId, "Unmarked Ticket = " + ticketId);
            await _actionsRepository.SaveChangesAsync();
        }
    }
}
