using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.TicketsDtos
{
    public record TicketSelectorDto
    {
        [Required]
        public int PageCount { get; init; }
        [Required]
        public int CurrentPage { get; init; }
        [Required]
        public TicketDto[] Tickets { get; init; }
    }
}
