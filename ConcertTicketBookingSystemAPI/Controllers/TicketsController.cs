using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.TicketsDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;
        private readonly ApplicationContext _context;
        public TicketsController(ILogger<ConcertsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<TicketDto>> GetTicketAsync(GetTicketDto dto)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == dto.TicketId);
            if (ticket != null) return ticket.ToDto();
            else return NotFound();
        }

        [HttpGet]
        [Route("many")]
        public async Task<ActionResult<TicketSelectorDto>> GetManyTicketsAsync(TicketSelectParametersDto dto)
        {
            var tickets = _context.Tickets.Where(t => dto.ByUserId == t.UserId);
            var ticketsCount = tickets.Count();
            if (ticketsCount > 0)
            {
                TicketSelectorDto selectorDto = new TicketSelectorDto()
                {
                    PageCount = ticketsCount / dto.NeededCount,
                    CurrentPage = dto.PageNumber,
                    Tickets = await tickets.Skip((dto.PageNumber - 1) * dto.NeededCount).Take(dto.NeededCount).ToDtosAsync()
                };
                return selectorDto;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddTicketAsync(AddTicketDto dto)
        {
            var ticket = dto.ToTicket();
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTicketAsync), new GetTicketDto() { TicketId = ticket.TicketId});
        }

        [HttpPost]
        [Route("Mark")]
        public async Task<ActionResult> MarkTicketAsync(MarkTicketDto dto)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == dto.TicketId);
            if (ticket != null && ticket.IsMarkedFlag == false)
            {
                ticket.IsMarkedFlag = true;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("Unmark")]
        public async Task<ActionResult> UnmarkTicketAsync(UnmarkTicketDto dto)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == dto.TicketId);
            if (ticket != null && ticket.IsMarkedFlag == true)
            {
                ticket.IsMarkedFlag = false;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}
