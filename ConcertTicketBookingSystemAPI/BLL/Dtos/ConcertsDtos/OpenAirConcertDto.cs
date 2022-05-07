using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ConcertsDtos
{
    public record OpenAirConcertDto
    {
        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 1)]
        public string Route { get; init; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string HeadLiner { get; init; }
    }
}
