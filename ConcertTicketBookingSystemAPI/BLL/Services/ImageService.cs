using BLL.Dtos.ImagesDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService : ICommonImagesService
    {
        private readonly IImagesRepository _imagesRepository;
        private readonly IAdditionalImagesRepository _additionalImagesRepository;
        public ImageService(IImagesRepository imagesRepository,
            IAdditionalImagesRepository additionalImagesRepository)
        {
            _imagesRepository = imagesRepository;
            _additionalImagesRepository = additionalImagesRepository;
        }

        public async Task<int> AddAdditionalImageAsync(AddImageDto dto)
        {
            var imageId = await _additionalImagesRepository.AddAdditionalImageAsync(dto.ConcertId, dto.Image, dto.ImageType); 
            await _additionalImagesRepository.SaveChangesAsync();
            return imageId;
        }

        public async Task<int> AddImageAsync(AddImageDto dto)
        {
            int imageId = await _imagesRepository.AddImageAsync(dto.Image, dto.ImageType);
            await _imagesRepository.SaveChangesAsync();
            return imageId;
        }

        public async Task<int> GetAdditionalImagesCountByConcertIdAsync(int concertId)
        {
            return await _additionalImagesRepository.GetAdditionalImagesCountByConcertIdAsync(concertId);
        }

        public async Task<ImageDto> GetImageByIdAsync(int imageId)
        {
            return (await _imagesRepository.GetImageByIdAsync(imageId)).ToDto();
        }
    }
}
