using BLL.Dtos.PersonalizationDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPersonalizationService
    {
        public Task UpdateBirthYearAsync(UpdateBirthYearDto dto, Guid userId);
        public Task UpdateNameAsync(UpdateNameDto dto, Guid userId);
        public Task UpdateEmailAsync(UpdateEmailDto dto, Guid userId);
        public Task RemoveRightsAsync(Guid userId);
        public Task ConfirmCookiesAsync(Guid userId);
        public Task ActivatePromoCodeAsync(ActivatePromocodeDto dto, Guid userId);
    }
}
