﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> AddConcertAsync(AddConcertDto dto)
        {
            if (dto.ConcertType == ConcertType.ClassicConcert) await _context.ClassicConcerts.AddAsync(dto.ToClassicConcert());
            else if (dto.ConcertType == ConcertType.OpenAirConcert) await _context.OpenAirConcerts.AddAsync(dto.ToOpenAirConcert());
            else await _context.PartyConcerts.AddAsync(dto.ToPartyConcert());
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        [Route("Activate")]
        public async Task<ActionResult> ActivateConcertAsync(ActivateConcertDto dto)
        {
            var concert = await _context.AbstractConcerts.FirstOrDefaultAsync(c => dto.ConcertId == c.ConcertId);
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
        public async Task<ActionResult> DeactivateConcertAsync(DeactivateConcertDto dto)
        {
            var concert = await _context.AbstractConcerts.FirstOrDefaultAsync(c => dto.ConcertId == c.ConcertId);
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
