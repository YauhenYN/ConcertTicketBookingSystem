using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ConcertsDtos
{
    public record ConcertSelectorDto
    {
        [Required]
        public LightConcertDto[] Concerts { get; init; }
        [Required]
        public int PagesCount { get; set; }
        [Required]
        public int CurrentPage { get; set; }
    }
}
