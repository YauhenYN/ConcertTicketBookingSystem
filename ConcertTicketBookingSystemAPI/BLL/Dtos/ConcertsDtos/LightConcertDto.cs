using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ConcertsDtos
{
    public record LightConcertDto
    {
        [Required]
        public int ConcertId { get; init; }
        [Required]
        public bool IsActiveFlag { get; init; }
        [Required]
        public int ImageId { get; init; }
        [Required]
        [Range(0.01, 1000)]
        public decimal Cost { get; init; }
        [Required]
        [Range(0, 10000)]
        public int LeftCount { get; init; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Performer { get; init; }
        [Required]
        public DateTime ConcertDate { get; init; }
        [Required]
        [Range(0, 2)]
        public ConcertType ConcertType { get; init; }
        [Required]
        public double Latitude { get; init; }
        [Required]
        public double Longitude { get; init; }
    }
}
