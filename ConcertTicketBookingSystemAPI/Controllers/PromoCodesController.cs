using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoCodesController : ControllerBase
    {
        private readonly ILogger<PromoCodesController> _logger;
        private readonly ApplicationContext _context;

        public PromoCodesController(ILogger<PromoCodesController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddPromoCodeAsync(AddPromoCodeDto dto)
        {
            if (!await _context.PromoCodes.AnyAsync(p => p.Code == dto.UniqueCode))
            {
                var promoCode = dto.ToPromoCode();
                await _context.PromoCodes.AddAsync(promoCode);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetManyPromoCodesAsync", new GetManyPromoCodesDto() { IsActive = dto.IsActive, Count = 1, ById = promoCode.PromoCodeId });
            }
            return Conflict();
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("many")]
        public async Task<ActionResult<PromoCodeDto[]>> GetManyPromoCodesAsync(GetManyPromoCodesDto dto)
        {
            IQueryable<PromoCode> promoCodes = _context.PromoCodes;
            if (dto.ById != null) promoCodes = promoCodes.Where(p => dto.ById == p.PromoCodeId);
            if (dto.ByCode != null) promoCodes = promoCodes.Where(p => dto.ByCode == p.Code);
            promoCodes = promoCodes.Where(p => dto.IsActive == p.IsActiveFlag).Take(dto.Count);
            if (promoCodes.Count() > 0) return await promoCodes.ToDtosAsync();
            else return NotFound();
        }

        [HttpPost]
        [Route("{promoCodeId}/Activate")]
        [Authorize]
        public async Task<ActionResult> ActivatePromoCodeAsync(Guid promoCodeId)
        {
            var ticket = await _context.PromoCodes.FirstOrDefaultAsync(p => p.PromoCodeId == promoCodeId);
            if (ticket != null && ticket.IsActiveFlag == false)
            {
                ticket.IsActiveFlag = true;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("{promoCodeId}/Deactivate")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeactivatePromoCodeAsync(Guid promoCodeId)
        {
            var ticket = await _context.PromoCodes.FirstOrDefaultAsync(p => p.PromoCodeId == promoCodeId);
            if (ticket != null && ticket.IsActiveFlag == true)
            {
                ticket.IsActiveFlag = false;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}
