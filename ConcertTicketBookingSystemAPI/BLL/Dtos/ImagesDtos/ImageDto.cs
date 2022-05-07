using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ImagesDtos
{
    public record ImageDto
    {
        [Required]
        public string ImageType { get; init; }
        [Required]
        public byte[] Source { get; init; }
        [Required]
        public int ImageId { get; init; }
    }
}
