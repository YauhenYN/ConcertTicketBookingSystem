using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace BLL.Dtos.PromoCodesDtos
{
    public static class ExtensionMethods
    {
        public static PromoCode ToPromoCode(this AddPromoCodeDto dto) => new PromoCode()
        {
            Code = dto.UniqueCode,
            Discount = dto.Discount,
            IsActiveFlag = dto.IsActiveFlag,
            LeftCount = dto.OnCount,
            TotalCount = dto.OnCount,
            PromoCodeId = Guid.NewGuid()
        };
        public static PromoCodeDto ToDto(this PromoCode promoCode) => new PromoCodeDto()
        {
            Discount = promoCode.Discount,
            IsActiveFlag = promoCode.IsActiveFlag,
            LeftCount = promoCode.LeftCount,
            UniqueCode = promoCode.Code,
            PromoCodeId = promoCode.PromoCodeId
        };
        public static Task<IEnumerable<PromoCodeDto>> ToDtosAsync(this IQueryable<PromoCode> promoCodes) => Task.FromResult(promoCodes.Select(p => new PromoCodeDto()
        {
            LeftCount = p.LeftCount,
            Discount = p.Discount,
            IsActiveFlag = p.IsActiveFlag,
            UniqueCode = p.Code,
            PromoCodeId = p.PromoCodeId
        }).AsEnumerable());
    }
}
