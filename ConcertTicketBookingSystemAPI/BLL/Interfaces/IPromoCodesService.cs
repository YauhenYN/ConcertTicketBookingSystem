using BLL.Dtos.PromoCodesDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPromoCodesService
    {
        public Task<IEnumerable<PromoCodeDto>> GetManyPromoCodesAsync(GetManyPromoCodesDto dto);
    }
}
