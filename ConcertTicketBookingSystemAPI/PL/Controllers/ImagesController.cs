using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BLL.Dtos.ImagesDtos;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : CustomControllerBase
    {
        private readonly ICommonImagesService _imagesService;
        private readonly IBasicOperationsConcertService _concertService;
        public ImagesController(ICommonImagesService imagesService, IBasicOperationsConcertService concertService)
        {
            _imagesService = imagesService;
            _concertService = concertService;
        }

        [HttpGet]
        [Route("{imageId}")]
        public async Task<FileContentResult> GetImageAsync(int imageId)
        {
            var image = await _imagesService.GetImageByIdAsync(imageId);
            if (image != null)
            {
                return new FileContentResult(image.Source, image.ImageType);
            }
            else return new FileContentResult(new byte[0], "image/png");
        }
        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddAdditionalImageAsync(AddImageDto dto)
        {
            if (!await _concertService.IsExistsAsync(dto.ConcertId) || await _imagesService.GetAdditionalImagesCountByConcertIdAsync(dto.ConcertId) > 4) return Conflict();
            var imageId = _imagesService.AddAdditionalImageAsync(dto);
            return CreatedAtRoute(imageId, new { imageId = imageId });
        }
    }
}
