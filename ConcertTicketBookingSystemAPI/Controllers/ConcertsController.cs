using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using ConcertTicketBookingSystemAPI.CustomServices;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;
        private readonly Models.ApplicationContext _context;
        private readonly EmailSenderService _senderService;
        private readonly PayPalPayment _payment;

        public ConcertsController(ILogger<ConcertsController> logger, Models.ApplicationContext context, EmailSenderService senderService, PayPalPayment payment)
        {
            _logger = logger;
            _context = context;
            _senderService = senderService;
            _payment = payment;
        }
        [HttpGet]
        [Authorize]
        [Route("{concertId}")]
        public async Task<ActionResult<ConcertDto>> GetConcertAsync(int concertId, AddConcertDto dto)
        {
            if(dto.ConcertType == ConcertType.ClassicConcert)
            {
                var concert = await _context.ClassicConcerts.Include(c => c.Images).FirstOrDefaultAsync(c => concertId == c.ConcertId);
                if (concert != null) return concert.ToDto();
                else return NotFound();
            }
            else if(dto.ConcertType == ConcertType.OpenAirConcert)
            {
                var concert = await _context.OpenAirConcerts.Include(c => c.Images).FirstOrDefaultAsync(c => concertId == c.ConcertId);
                if (concert != null) return concert.ToDto();
                else return NotFound();
            }
            else
            {
                var concert = await _context.PartyConcerts.Include(c => c.Images).FirstOrDefaultAsync(c => concertId == c.ConcertId);
                if (concert != null) return concert.ToDto();
                else return NotFound();
            }
        }
        //[HttpPost]
        //[Authorize]
        //[Route("{concertId}/Buy/PayPal")]
        //public async Task<ActionResult> BuyTicket_PayPalAsync(int ConcertId, BuyTicketDto dto)
        //{
        //    await _senderService.SendHtmlAsync("Ticket", "Emailgdsgs", "ticket information");
        //}

        [HttpGet]
        [Route("many/light")]
        [Authorize]
        public async Task<ActionResult<ConsertSelectorDto>> GetManyLightConcertsAsync(ConcertSelectParametersDto dto)
        {
            IQueryable<Models.Concert> concerts;
            if(dto.ByConcertType == ConcertType.ClassicConcert) concerts = _context.ClassicConcerts;
            else if(dto.ByConcertType == ConcertType.OpenAirConcert) concerts = _context.OpenAirConcerts;
            else concerts = _context.PartyConcerts;
            concerts = concerts.Where(c => c.Performer == dto.ByPerformer).Where(c => c.Cost < dto.UntilPrice && c.Cost > dto.FromPrice);
            var concertsCount = concerts.Count();
            if (concertsCount > 0)
            {
                ConsertSelectorDto selector = new ConsertSelectorDto()
                {
                    PagesCount = concertsCount / dto.NeededCount,
                    CurrentPage = dto.NextPage,
                    Concerts = await concerts.Skip((dto.NextPage - 1) * dto.NeededCount).Take(dto.NeededCount).ToDtosAsync(dto.ByConcertType)
                };
                return selector;
            }
            else return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddConcertAsync(AddConcertDto dto)
        {
            Models.Concert concert;
            if (dto.ConcertType == ConcertType.ClassicConcert)
            {
                concert = dto.ToClassicConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.ClassicConcerts.AddAsync((Models.ClassicConcert)concert);
            }
            else if (dto.ConcertType == ConcertType.OpenAirConcert)
            {
                concert = dto.ToOpenAirConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.OpenAirConcerts.AddAsync((Models.OpenAirConcert)concert);
            }
            else
            {
                concert = dto.ToPartyConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.PartyConcerts.AddAsync((Models.PartyConcert)concert);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetConcertAsync", new { concertId = concert.ConcertId, getConcertDto = new GetConcertDto { ConcertType = dto.ConcertType } });
        }
        [HttpPost]
        [Route("{concertId}/Activate")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ActivateConcertAsync(int concertId)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(c => concertId == c.ConcertId);
            if (concert != null && concert.IsActiveFlag == false)
            {
                concert.IsActiveFlag = true;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("{concertId}/Deactivate")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeactivateConcertAsync(int concertId)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(c => concertId == c.ConcertId);
            if (concert != null && concert.IsActiveFlag == true)
            {
                concert.IsActiveFlag = false;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}
