using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BLL.Dtos.TicketsDtos;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : CustomControllerBase
    {
        private readonly ICommonTicketsService _ticketsService;
        private readonly IConcertService _concertService;
        public TicketsController(ICommonTicketsService ticketsService, IConcertService concertService)
        {
            _ticketsService = ticketsService;
            _concertService = concertService;
        }

        [HttpGet]
        [Route("{ticketId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<TicketDto>> GetTicketAsync(Guid ticketId)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(ticketId);
            if (ticket != null) return ticket;
            else return NotFound();
        }

        [HttpGet]
        [Route("many")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<TicketSelectorDto>> GetManyTicketsAsync([FromQuery]TicketSelectParametersDto dto)
        {
            var selector = await _ticketsService.GetManyTicketsAsync(dto, UserId.Value);
            if (selector.Tickets.Length > 0) return selector;
            return NotFound();
        }

        [HttpPost]
        [Route("{ticketId}/Mark")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> MarkTicketAsync(Guid ticketId)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(ticketId);
            var ticketConcert = await _concertService.GetConcertByIdAsync(ticket.ConcertId, UserId.Value);
            if (ticket != null && ticket.IsMarked == false && ticketConcert.ConcertDate < DateTime.Now.ToUniversalTime() && ticketConcert.IsActiveFlag)
            {
                await _ticketsService.MarkTicketAsync(ticketId, UserId.Value);
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("{ticketId}/Unmark")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UnmarkTicketAsync(Guid ticketId)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(ticketId);
            var ticketConcert = await _concertService.GetConcertByIdAsync(ticket.ConcertId, UserId);
            if (ticket != null && ticket.IsMarked == true && ticketConcert.ConcertDate < DateTime.Now.ToUniversalTime() && ticketConcert.IsActiveFlag)
            {
                await _ticketsService.UnmarkTicketAsync(ticketId, UserId.Value);
                return NoContent();
            }
            else return NotFound();
        }
    }
}
