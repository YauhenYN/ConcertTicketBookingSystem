using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.TicketsDtos;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;

        public TicketsController(ILogger<ConcertsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<TicketDto>> GetTicketAsync(GetTicketDto dto)
        {
            return Ok();
        }

        [HttpGet]
        [Route("many")]
        public async Task<ActionResult<TicketSelectorDto>> GetManyTicketsAsync(TicketSelectParametersDto dto)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddTicket(AddTicketDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Mark")]
        public async Task<ActionResult> MarkTicket(MarkTicketDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Unmark")]
        public async Task<ActionResult> UnmarkTicket(UnmarkTicketDto dto)
        {
            return Ok();
        }
    }
}
