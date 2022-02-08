using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService
{
    public interface IConfirmationService<T>
    {
        public void Add(T confirmationItem, Action onConfirmationAction);
        public bool Confirm(T confirmationItem);
    }
}
