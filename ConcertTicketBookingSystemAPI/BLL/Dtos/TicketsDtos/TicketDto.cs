using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.TicketsDtos
{
    public record TicketDto
    {
        [Required]
        public Guid TicketId { get; init; }
        [Required]
        public Guid UserId { get; init; }
        [Required]
        [Range(1, 5)]
        public int OnCount { get; init; }
        [Required]
        public bool IsMarked { get; init; }
        public Guid? PromoCode { get; init; }
        [Required]
        public int ConcertId { get; init; }
        [Required]
        public string ConcertPerformer { get; init; }
    }
}
