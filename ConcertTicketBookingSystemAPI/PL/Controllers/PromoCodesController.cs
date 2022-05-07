using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Dtos.PromoCodesDtos;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using BLL.Interfaces;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoCodesController : CustomControllerBase
    {
        ICommonPromoCodesService _promoCodesService;
        public PromoCodesController(ICommonPromoCodesService promoCodesService)
        {
            _promoCodesService = promoCodesService;
        }

        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddPromoCodeAsync(AddPromoCodeDto dto)
        {
            if (!await _promoCodesService.AnyWithSuchCode(dto.UniqueCode))
            {
                var promoCodeId = await _promoCodesService.AddPromoCodeAsync(dto, UserId);
                return CreatedAtAction("GetPromoCodeById", new 
                { 
                    promoCodeId = promoCodeId
                });
            }
            return Conflict();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("id")]
        public async Task<ActionResult<PromoCodeDto>> GetPromoCodeByIdAsync(Guid promoCodeId)
        {
            var promoCode = await _promoCodesService.GetPromoCodeByIdAsync(promoCodeId);
            if (promoCode != null) return promoCode;
            else return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [Route("code")]

        public async Task<ActionResult<PromoCodeDto>> GetPromoCodeByCodeAsync([FromQuery]GetPromoCodeByCode dto)
        {
            var promoCode = await _promoCodesService.GetPromoCodeByCodeAsync(dto.Code);
            if (promoCode != null) return promoCode;
            else return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [Route("many")]
        public async Task<ActionResult<IEnumerable<PromoCodeDto>>> GetManyPromoCodesAsync([FromQuery]GetManyPromoCodesDto dto)
        {
            var promoCodes = await _promoCodesService.GetManyPromoCodesAsync(dto);
            if (promoCodes.Count() > 0) return promoCodes.ToArray();
            else return NotFound();
        }

        [HttpPost]
        [Route("{promoCodeId}/Activate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> ActivatePromoCodeAsync(Guid promoCodeId)
        {
            var ticket = await _promoCodesService.GetPromoCodeByIdAsync(promoCodeId);
            if (ticket != null && ticket.IsActiveFlag == false)
            {
                await _promoCodesService.ActivatePromoCodeAsync(promoCodeId, UserId);
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("{promoCodeId}/Deactivate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> DeactivatePromoCodeAsync(Guid promoCodeId)
        {
            var ticket = await _promoCodesService.GetPromoCodeByIdAsync(promoCodeId);
            if (ticket != null && ticket.IsActiveFlag == true)
            {
                await _promoCodesService.DeactivatePromoCodeAsync(promoCodeId, UserId);
                return NoContent();
            }
            else return NotFound();
        }
    }
}
