using BLL.Dtos.ConcertsDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IConcertService : IBasicOperationsConcertService
    {
        public Task<ConcertDto> GetConcertByIdAsync(int concertId, Guid userId);
        public Task<int> AddConcertAsync(AddConcertDto dto, Guid userId);
        public Task ActivateConcertAsync(int concertId, Guid userId);
        public Task DeactivateConcertAsync(int concertId, Guid userId);
    }
}
