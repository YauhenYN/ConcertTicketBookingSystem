using BLL.Dtos.PromoCodesDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PromoCodesService : ICommonPromoCodesService
    {
        public IPromoCodesRepository _promoCodesRepository;
        public IActionsRepository _actionsRepository;
        public PromoCodesService(IPromoCodesRepository promoCodesRepository,
            IActionsRepository actionsRepository)
        {
            _promoCodesRepository = promoCodesRepository;
            _actionsRepository = actionsRepository;
        }

        public async Task ActivatePromoCodeAsync(Guid promoCodeId, Guid userId)
        {
            var promoCode = await _promoCodesRepository.GetByIdAsync(promoCodeId);
            promoCode.IsActiveFlag = true;
            await _actionsRepository.AddActionAsync(userId, "Activated PromoCode = " + promoCodeId);
            await _actionsRepository.SaveChangesAsync();
        }

        public async Task<Guid> AddPromoCodeAsync(AddPromoCodeDto dto, Guid userId)
        {
            var promoCode = dto.ToPromoCode();
            await _promoCodesRepository.AddAsync(promoCode);
            await _actionsRepository.AddActionAsync(userId, "Added PromoCode");
            await _actionsRepository.SaveChangesAsync();
            return promoCode.PromoCodeId;
        }

        public Task<bool> AnyWithSuchCode(string code)
        {
            return _promoCodesRepository.IsExistsByCodeAsync(code);
        }

        public async Task DeactivatePromoCodeAsync(Guid promoCodeId, Guid userId)
        {
            var promoCode = await _promoCodesRepository.GetByIdAsync(promoCodeId);
            promoCode.IsActiveFlag = false;
            await _actionsRepository.AddActionAsync(userId, "Deactivated PromoCode = " + promoCodeId);
            await _actionsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PromoCodeDto>> GetManyPromoCodesAsync(GetManyPromoCodesDto dto)
        {
            IQueryable<PromoCode> promoCodes = _promoCodesRepository.GetQueryable();
            promoCodes = promoCodes.Where(p => dto.IsActiveFlag == p.IsActiveFlag).Take(dto.Count);
            return await promoCodes.ToDtosAsync();
        }

        public async Task<PromoCodeDto> GetPromoCodeByCodeAsync(string code)
        {
            var promoCode = await _promoCodesRepository.GetByCodeAsync(code);
            return promoCode == null ? null : promoCode.ToDto();
        }

        public async Task<PromoCodeDto> GetPromoCodeByIdAsync(Guid promoCodeId)
        {
            var promoCode = await _promoCodesRepository.GetByIdAsync(promoCodeId);
            return promoCode == null ? null : promoCode.ToDto();
        }
    }
}
