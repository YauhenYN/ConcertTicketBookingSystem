using BLL.Dtos.PromoCodesDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPromoCodeService
    {
        public Task<PromoCodeDto> GetPromoCodeByIdAsync(Guid promoCodeId);
        public Task<PromoCodeDto> GetPromoCodeByCodeAsync(string code);
        public Task<Guid> AddPromoCodeAsync(AddPromoCodeDto dto, Guid userId);
        public Task ActivatePromoCodeAsync(Guid promoCodeId, Guid userId);
        public Task DeactivatePromoCodeAsync(Guid promoCodeId, Guid userId);
        public Task<bool> AnyWithSuchCode(string code);
    }
}
