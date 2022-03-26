using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService
{
    public interface IConfirmationService<T, V>
    {
        public void Add(T confirmationItem, Action<V> onConfirmationAction);
        public bool Confirm(T confirmationItem, V into);
    }
}
