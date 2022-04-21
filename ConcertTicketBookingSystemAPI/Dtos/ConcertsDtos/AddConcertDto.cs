using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record AddConcertDto : IValidatableObject
    {
        [Required]
        public bool IsActiveFlag { get; init; }
        [Required]
        [MaxLength(50)]
        public string ImageType { get; init; }
        [Required]
        [MaxLength(200000)]
        public string Image { get; init; }
        [Required]
        [Range(0.01, 1000)]
        public decimal Cost { get; init; }
        [Required]
        [Range(1, 10000)]
        public int TotalCount { get; init; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Performer { get; init; }
        [Required]
        public DateTime ConcertDate { get; init; }
        [Required]
        public double Latitude { get; init; }
        [Required]
        public double Longitude { get; init; }
        [Required]
        [Range(0, 2)]
        public ConcertType ConcertType { get; init; }
        public ClassicConcertDto ClassicConcertInfo { get; init; }
        public OpenAirConcertDto OpenAirConcertInfo { get; init; }
        public PartyConcertDto PartyConcertInfo { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var list = new List<ValidationResult>();
            if (ImageType != "image/png" && ImageType != "image/jpeg") list.Add(new ValidationResult("Неверный формат изображения"));
            if (ClassicConcertInfo == null && OpenAirConcertInfo == null && PartyConcertInfo == null) list.Add(new ValidationResult("Должен быть определён весь тип"));
            if (ConcertDate < DateTime.UtcNow || ConcertDate > DateTime.UtcNow.AddDays(100)) list.Add(new ValidationResult("Истекшая либо старше 100 дней дата"));
            return list;
        }
    }
}
