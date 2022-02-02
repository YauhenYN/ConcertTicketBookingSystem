using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos;
using ConcertTicketBookingSystemAPI.Models;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonalizationController : ControllerBase
    {
        private readonly ILogger<PersonalizationController> _logger;
        private readonly ApplicationContext _context;

        public PersonalizationController(ILogger<PersonalizationController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateBirthYearAsync(UpdateBirthYearDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateNameAsync(UpdateNameDto dto)
        {
            return Ok();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateEmailAsync(UpdateEmailDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RemoveRightsAsync()
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ActivatePromocodeAsync(ActivatePromocodeDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> BuyTicket_PayPalAsync(BuyTicketDto dto)
        {
            return Ok();
        }
    }
}
