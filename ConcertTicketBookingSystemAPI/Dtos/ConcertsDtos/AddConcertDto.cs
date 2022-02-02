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
        public bool IsActive { get; init; }
        [Required]
        public string PreImageType { get; init; }
        [Required]
        [Range(100, 50000)] //still don't khow how much an image must have weigh
        public byte[] PreImage { get; init; }
        [Required]
        public decimal Cost { get; init; }
        [Required]
        public int TotalTicketsCount { get; init; }
        [Required]
        public string Performer { get; init; }
        [Required]
        public DateTime EventTime { get; init; }
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
            if (ClassicConcertInfo == null && OpenAirConcertInfo == null && PartyConcertInfo == null) list.Add(new ValidationResult("Должен быть определён весь тип"));
            return list; 
        }
    }
}
