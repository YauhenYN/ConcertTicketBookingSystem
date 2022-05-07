using DAL.Models;
using System;

namespace BLL.Dtos.ImagesDtos
{
    public static class ExtensionMethods
    {
        public static AdditionalImage ToAdditionalImage(this AddImageDto dto) => new AdditionalImage()
        {
            ConcertId = dto.ConcertId,
            Image = new Image()
            {
                Type = dto.ImageType,
                Source = Convert.FromBase64String(dto.Image)
            }
        };
        public static Image ToConcertImage(this AddImageDto dto) => new Image()
        {
            Type = dto.ImageType,
            Source = Convert.FromBase64String(dto.Image)
        };
        public static ImageDto ToDto(this Image image) => new ImageDto()
        {
            ImageId = image.ImageId,
            Source = image.Source,
            ImageType = image.Type
        };
    }
}
