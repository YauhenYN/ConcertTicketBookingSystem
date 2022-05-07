using BLL.Dtos.ImagesDtos;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IImageService
    {
        public Task<int> AddImageAsync(AddImageDto dto);
        public Task<ImageDto> GetImageByIdAsync(int imageId);
    }
}
