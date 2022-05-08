using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BLL.Dtos.PersonalizationDtos;
using Microsoft.AspNetCore.Authorization;
using BLL.Dtos.UsersDtos;
using BLL.Interfaces;
using BLL.Dtos.PromoCodesDtos;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PersonalizationController : CustomControllerBase
    {
        private readonly IPersonalizationService _personalizationService;
        private readonly IUserInfoService _userInfoService;
        private readonly IPromoCodeService _promoCodeService;

        public PersonalizationController(IPersonalizationService personalizationService,
            IUserInfoService userInfoService,
            IPromoCodeService promoCodeService)
        {
            _personalizationService = personalizationService;
            _userInfoService = userInfoService;
            _promoCodeService = promoCodeService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateBirthYearAsync(UpdateBirthYearDto dto)
        {
            await _personalizationService.UpdateBirthYearAsync(dto, UserId.Value);
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateNameAsync(UpdateNameDto dto)
        {
            await _personalizationService.UpdateNameAsync(dto, UserId.Value);
            return NoContent();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateEmailAsync(UpdateEmailDto dto)
        {
            await _personalizationService.UpdateEmailAsync(dto, UserId.Value);
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RemoveRightsAsync()
        {
            await _personalizationService.RemoveRightsAsync(UserId.Value);
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ActivatePromocodeAsync(ActivatePromocodeDto dto)
        {
            var user = await _userInfoService.GetUserInfoByIdAsync(UserId.Value);
            PromoCodeDto userPromoCode = null;
            if (user.PromoCodeId != null) userPromoCode = await _promoCodeService.GetPromoCodeByIdAsync(user.PromoCodeId.Value);
            var promoCode = await _promoCodeService.GetPromoCodeByCodeAsync(dto.Code);
            if(promoCode != null && (userPromoCode == null || promoCode.Discount > userPromoCode.Discount) && promoCode.LeftCount > 0)
            {
                return NoContent();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ConfirmCookiesAsync()
        {
            var user = await _userInfoService.GetUserInfoByIdAsync(UserId.Value);
            if (user.CookieConfirmationFlag == true) return Conflict();
            else
            {
                await _personalizationService.ConfirmCookiesAsync(UserId.Value);
                return NoContent();
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<UserInfoDto>> UserInfoAsync()
        {
            return await _userInfoService.GetUserInfoByIdAsync(UserId.Value);
        }
    }
}
