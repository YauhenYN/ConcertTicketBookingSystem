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
using ConcertTicketBookingSystemAPI.Helpers;

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
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddPromoCodeAsync(AddPromoCodeDto dto)
        {
            if (!await _context.PromoCodes.AnyAsync(p => p.Code == dto.UniqueCode))
            {
                var promoCode = dto.ToPromoCode();
                await _context.PromoCodes.AddAsync(promoCode);
                await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Added PromoCode");
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPromoCodeByIdAsync", promoCode.PromoCodeId);
            }
            return Conflict();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("id")]
        public async Task<ActionResult<PromoCodeDto>> GetPromoCodeByIdAsync(Guid promoCodeId)
        {
            var promoCode = await _context.PromoCodes.FirstOrDefaultAsync(p => promoCodeId == p.PromoCodeId);
            if (promoCode != null) return promoCode.ToDto();
            else return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [Route("code")]

        public async Task<ActionResult<PromoCodeDto>> GetPromoCodeByCodeAsync(GetPromoCodeByCode dto)
        {
            var promoCode = await _context.PromoCodes.FirstOrDefaultAsync(p => dto.Code == p.Code);
            if (promoCode != null) return promoCode.ToDto();
            else return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [Route("many")]
        public async Task<ActionResult<PromoCodeDto[]>> GetManyPromoCodesAsync(GetManyPromoCodesDto dto)
        {
            IQueryable<PromoCode> promoCodes = _context.PromoCodes;
            promoCodes = promoCodes.Where(p => dto.IsActiveFlag == p.IsActiveFlag).Take(dto.Count);
            if (promoCodes.Count() > 0) return await promoCodes.ToDtosAsync();
            else return NotFound();
        }

        [HttpPost]
        [Route("{promoCodeId}/Activate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> ActivatePromoCodeAsync(Guid promoCodeId)
        {
            var ticket = await _context.PromoCodes.FirstOrDefaultAsync(p => p.PromoCodeId == promoCodeId);
            if (ticket != null && ticket.IsActiveFlag == false)
            {
                ticket.IsActiveFlag = true;
                await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Activated PromoCode = " + promoCodeId);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("{promoCodeId}/Deactivate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> DeactivatePromoCodeAsync(Guid promoCodeId)
        {
            var ticket = await _context.PromoCodes.FirstOrDefaultAsync(p => p.PromoCodeId == promoCodeId);
            if (ticket != null && ticket.IsActiveFlag == true)
            {
                ticket.IsActiveFlag = false;
                await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Deactivated PromoCode = " + promoCodeId);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}
