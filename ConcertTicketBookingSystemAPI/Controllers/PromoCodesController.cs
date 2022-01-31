using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoCodesController : ControllerBase
    {
        private readonly ILogger<PromoCodesController> _logger;

        public PromoCodesController(ILogger<PromoCodesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> AddPromoCodeAsync(AddPromoCodeDto dto)
        {
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<PromoCodeDto[]>> GetManyPromoCodesAsync(GetManyPromoCodesDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Activate")]
        public async Task<ActionResult> ActivatePromoCodeAsync(ActivatePromoCodeDto dto)
        {

            return Ok();
        }

        [HttpPost]
        [Route("Deactivate")]
        public async Task<ActionResult> DeactivatePromoCodeAsync(DeactivatePromoCodeDto dto)
        {

            return Ok();
        }
    }
}
