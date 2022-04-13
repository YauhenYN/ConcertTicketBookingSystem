using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.TicketsDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ConcertTicketBookingSystemAPI.Helpers;

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
        [Route("{ticketId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<TicketDto>> GetTicketAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
            //(await _context.Users.Include(u => u.Tickets).FirstAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name)))
            if (ticket != null) return ticket.ToDto();
            else return NotFound();
        }

        [HttpGet]
        [Route("many")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<TicketSelectorDto>> GetManyTicketsAsync([FromQuery]TicketSelectParametersDto dto)
        {
            IQueryable<Ticket> tickets = _context.Tickets.Include(t => t.Concert);
            if (dto.ByUserId != null) tickets = tickets.Where(t => dto.ByUserId == t.UserId);
            if (dto.ByConcertId != null) tickets = tickets.Where(t => t.ConcertId == dto.ByConcertId);
            var ticketsCount = tickets.Count();
            if (ticketsCount > 0)
            {
                TicketSelectorDto selectorDto = new TicketSelectorDto()
                {
                    PageCount = (ticketsCount / dto.NeededCount) + 1,
                    CurrentPage = dto.PageNumber,
                    Tickets = await tickets.Skip(dto.PageNumber * dto.NeededCount).Take(dto.NeededCount).ToDtosAsync()
                };
                return selectorDto;
            }
            return NotFound();
        }

        [HttpPost]
        [Route("{ticketId}/Mark")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> MarkTicketAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
            if (ticket != null && ticket.IsMarkedFlag == false)
            {
                ticket.IsMarkedFlag = true;
                await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Marked Ticket = " + ticketId);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("{ticketId}/Unmark")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UnmarkTicketAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
            if (ticket != null && ticket.IsMarkedFlag == true)
            {
                ticket.IsMarkedFlag = false;
                await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Unmarked Ticket = " + ticketId);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}
