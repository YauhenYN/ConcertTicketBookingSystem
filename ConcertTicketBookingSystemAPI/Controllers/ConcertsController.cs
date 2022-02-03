using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;
        private readonly ApplicationContext _context;

        public ConcertsController(ILogger<ConcertsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ConcertDto>> GetConcertAsync(GetConcertDto dto)
        {
            if(dto.ConcertType == ConcertType.ClassicConcert)
            {
                var concert = await _context.ClassicConcerts.Include(c => c.Images).FirstOrDefaultAsync(c => dto.ConcertId == c.ConcertId);
                if (concert != null) return concert.ToDto();
                else return NotFound();
            }
            else if(dto.ConcertType == ConcertType.OpenAirConcert)
            {
                var concert = await _context.OpenAirConcerts.Include(c => c.Images).FirstOrDefaultAsync(c => dto.ConcertId == c.ConcertId);
                if (concert != null) return concert.ToDto();
                else return NotFound();
            }
            else
            {
                var concert = await _context.PartyConcerts.Include(c => c.Images).FirstOrDefaultAsync(c => dto.ConcertId == c.ConcertId);
                if (concert != null) return concert.ToDto();
                else return NotFound();
            }
        }
        [HttpGet]
        [Route("light")]
        [Authorize]
        public async Task<ActionResult<ConsertSelectorDto>> GetManyLightConcertsAsync(ConcertSelectParametersDto dto)
        {
            IQueryable<Concert> concerts;
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
            Concert concert;
            if (dto.ConcertType == ConcertType.ClassicConcert)
            {
                concert = dto.ToClassicConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.ClassicConcerts.AddAsync((ClassicConcert)concert);
            }
            else if (dto.ConcertType == ConcertType.OpenAirConcert)
            {
                concert = dto.ToOpenAirConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.OpenAirConcerts.AddAsync((OpenAirConcert)concert);
            }
            else
            {
                concert = dto.ToPartyConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.PartyConcerts.AddAsync((PartyConcert)concert);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetConcertAsync", new GetConcertDto { ConcertId = concert.ConcertId, ConcertType = dto.ConcertType});
        }
        [HttpPost]
        [Route("Activate")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ActivateConcertAsync(ActivateConcertDto dto)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(c => dto.ConcertId == c.ConcertId);
            if (concert != null && concert.IsActiveFlag == false)
            {
                concert.IsActiveFlag = true;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("Deactivate")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeactivateConcertAsync(DeactivateConcertDto dto)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(c => dto.ConcertId == c.ConcertId);
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
