using BLL.Dtos.ImagesDtos;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdditionalImagesService
    {
        public Task<int> AddAdditionalImageAsync(AddImageDto dto);
        public Task<int> GetAdditionalImagesCountByConcertIdAsync(int concertId);
    }
}
