using BLL.Dtos.ConcertsDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IConcertPaymentService
    {
        public Task<string> PrePayAsync(int concertId, BuyTicketDto dto, Guid userId);
        public Task ConfirmedPaymentAsync(string token, Guid userId);
    }
}
