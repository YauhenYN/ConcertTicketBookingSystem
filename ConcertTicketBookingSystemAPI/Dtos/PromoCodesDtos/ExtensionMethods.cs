using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos
{
    public static class ExtensionMethods
    {
        public static PromoCode ToPromoCode(this AddPromoCodeDto dto) => new PromoCode() { Code = dto.UniqueCode, Discount = dto.Discount, IsActiveFlag = dto.IsActiveFlag, LeftCount = dto.OnCount, TotalCount = dto.OnCount, PromoCodeId = Guid.NewGuid() };
        public static PromoCodeDto ToDto(this PromoCode promoCode) => new PromoCodeDto()
        {
            Discount = promoCode.Discount,
            IsActiveFlag = promoCode.IsActiveFlag,
            LeftCount = promoCode.LeftCount,
            UniqueCode = promoCode.Code
        };
        public static async Task<PromoCodeDto[]> ToDtosAsync(this IQueryable<PromoCode> promoCodes) => await promoCodes.Select(p => new PromoCodeDto() { LeftCount = p.LeftCount, Discount = p.Discount, IsActiveFlag = p.IsActiveFlag, UniqueCode = p.Code }).ToArrayAsync();
    }
}
