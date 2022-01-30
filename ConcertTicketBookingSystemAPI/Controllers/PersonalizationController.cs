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
        public async Task<IActionResult> UpdateBirthYear(UpdateBirthYearDto dto)
        {
            return Content(dto.BirthYear.ToString());
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateName(UpdateNameDto dto)
        {
            return Ok();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEmail(UpdateEmailDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RemoveRights()
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ActivatePromocode(ActivatePromocodeDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> BuyTicket_PayPal(BuyTicketDto dto)
        {
            return Ok();
        }
    }
}
