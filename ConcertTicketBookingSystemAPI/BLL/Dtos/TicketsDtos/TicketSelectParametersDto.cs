using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.TicketsDtos
{
    public record TicketSelectParametersDto
    {
        [Required]
        public int PageNumber { get; init; }
        public Guid? ByUserId { get; init; }
        public int? ByConcertId { get; init; }
        [StringLength(36, MinimumLength = 1)]
        public string ByTicketId { get; init; }
        [Required]
        public int NeededCount { get; set; }
    }
}
