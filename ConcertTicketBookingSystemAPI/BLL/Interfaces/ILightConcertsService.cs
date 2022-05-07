using BLL.Dtos.ConcertsDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILightConcertsService
    {
        public Task<ConcertSelectorDto> GetManyLightConcertsAsync(ConcertSelectParametersDto selectParametersDto, Guid userId);
    }
}
