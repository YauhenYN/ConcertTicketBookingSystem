using BLL.Dtos.TicketsDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITicketsService
    {
        public Task<TicketSelectorDto> GetManyTicketsAsync(TicketSelectParametersDto dto, Guid userId);
    }
}
