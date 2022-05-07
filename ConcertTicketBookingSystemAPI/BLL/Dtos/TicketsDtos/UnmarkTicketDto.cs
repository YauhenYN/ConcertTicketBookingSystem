using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.TicketsDtos
{
    public record UnmarkTicketDto
    {
        [Required]
        public Guid TicketId { get; init; }
    }
}
