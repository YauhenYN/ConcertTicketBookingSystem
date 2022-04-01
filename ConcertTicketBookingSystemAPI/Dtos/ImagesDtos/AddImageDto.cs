using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ImagesDtos
{
    public record AddImageDto : IValidatableObject
    {
        [Required]
        public string ImageType { get; init; }
        [Required]
        public string Image { get; init; }
        public int ConcertId { get; init; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var list = new List<ValidationResult>();
            if (ImageType != "image/png" && ImageType != "image/jpeg") list.Add(new ValidationResult("Неверный формат изображения"));
            if (Image.Length > 200000) list.Add(new ValidationResult("Слишком большое изображение"));
            return list;
        }
    }
}
