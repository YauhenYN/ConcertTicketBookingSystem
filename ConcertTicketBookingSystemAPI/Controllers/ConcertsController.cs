using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;

        public ConcertsController(ILogger<ConcertsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ConcertDto>> GetConcertAsync(GetConcertDto dto)
        {
            return Ok();
        }
        [HttpGet]
        [Route("light")]
        public async Task<ActionResult<ConsertSelectorDto>> GetManyLightConcertsAsync(ConcertSelectParametersDto dto)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> AddConcertAsync(AddConcertDto dto)
        {
            return Ok();
        }
        [HttpPost]
        [Route("Activate")]
        public async Task<ActionResult> ActivateConcertAsync(ActivateConcertDto dto)
        {
            return Ok();
        }
        [HttpPost]
        [Route("Deactivate")]
        public async Task<ActionResult> DeactivateConcertAsync(DeactivateConcertDto dto)
        {
            return Ok();
        }
    }
}
