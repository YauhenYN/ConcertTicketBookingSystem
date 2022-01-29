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
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateBirthYear(UpdateBirthYearDto dto)
        {
            return Content(dto.BirthYear.ToString());
        }
    }
}
