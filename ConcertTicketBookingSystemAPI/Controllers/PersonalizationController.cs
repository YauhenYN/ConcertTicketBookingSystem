using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonalizationController : ControllerBase
    {
        private readonly ILogger<PersonalizationController> _logger;

        public PersonalizationController(ILogger<PersonalizationController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateBirthYearAsync(UpdateBirthYearDto dto)
        {
            return Content(dto.BirthYear.ToString());
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
