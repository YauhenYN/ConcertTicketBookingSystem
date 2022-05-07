using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BLL.Dtos.ConcertsDtos;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;
using BLL.Configurations;
using Microsoft.Extensions.Options;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertsController : CustomControllerBase
    {

        private readonly ICommonConcertsService _commonConcertsService;
        private readonly IConcertPaymentService _concertPaymentService;
        private readonly PayPalConf _payPalConfiguration;

        public ConcertsController(ICommonConcertsService commonConcertsService,
            IConcertPaymentService concertPaymentService, 
            IOptions<PayPalConf> options)
        {
            _commonConcertsService = commonConcertsService;
            _concertPaymentService = concertPaymentService;
            _payPalConfiguration = options.Value;
        }

        [HttpGet]
        [Route("{concertId}")]
        public async Task<ActionResult<ConcertDto>> GetConcertAsync(int concertId)
        {
            var concert = await _commonConcertsService.GetConcertByIdAsync(concertId, UserId);
            if (concert != null) return concert;
            else return NotFound();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("{concertId}/Buy/PayPal")]
        public async Task<ActionResult<string>> BuyTicket_PayPalAsync(int concertId, BuyTicketDto dto)
        {
            var concertInfo = await _commonConcertsService.GetConcertByIdAsync(concertId, UserId);
            if (concertInfo != null && concertInfo.IsActiveFlag == true && concertInfo.LeftTicketsCount > 0)
            {
                var approveUrl = await _concertPaymentService.PrePayAsync(concertId, dto, UserId);
                if (approveUrl != null) return approveUrl;
                else return Ok();
            }
            else return NotFound();
        }

        [HttpGet]
        [Route("Buy/PayPal")]
        public async Task<ActionResult> BuyTicket_PayPalAsync([FromQuery] string token, string payerID)
        {
            //this is where actual transaction is carried out
            try
            {
                await _concertPaymentService.ConfirmedPaymentAsync(token, UserId);
                return Redirect(_payPalConfiguration.SuccessURL);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpGet]
        [Route("many/light")]
        public async Task<ActionResult<ConcertSelectorDto>> GetManyLightConcertsAsync([FromQuery] ConcertSelectParametersDto dto)
        {
            var lightConcerts = await _commonConcertsService.GetManyLightConcertsAsync(dto, UserId);
            if (lightConcerts.Concerts.Length > 0) return lightConcerts;
            else return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddConcertAsync(AddConcertDto dto)
        {
            var concertId = await _commonConcertsService.AddConcertAsync(dto, UserId);
            return CreatedAtRoute(concertId, new { concertId = concertId });
        }

        [HttpPost]
        [Route("{concertId}/Activate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> ActivateConcertAsync(int concertId)
        {
            var concert = await _commonConcertsService.GetConcertByIdAsync(concertId, UserId);
            if (concert != null && concert.IsActiveFlag == false && concert.ConcertDate > DateTime.UtcNow)
            {
                await _commonConcertsService.ActivateConcertAsync(concertId, UserId);
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("{concertId}/Deactivate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> DeactivateConcertAsync(int concertId)
        {
            var concert = await _commonConcertsService.GetConcertByIdAsync(concertId, UserId);
            if (concert != null && concert.IsActiveFlag == true && concert.ConcertDate > DateTime.UtcNow)
            {
                await _commonConcertsService.DeactivateConcertAsync(concertId, UserId);
                return NoContent();
            }
            else return NotFound();
        }
    }
}
