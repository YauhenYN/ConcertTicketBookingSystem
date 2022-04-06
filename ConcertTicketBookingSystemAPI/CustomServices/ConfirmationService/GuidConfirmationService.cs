using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService
{
    public class ConfirmationService<E, T> : IConfirmationService<E, T>, IDisposable where E : IComparable
    {
        private class ExpirationElement
        {
            public DateTime CreationTime { get; }
            public E ConfirmationCode { get; }
            public Action<T> OnConfirmationAction { get; }
            public ExpirationElement(E confirmationCode, Action<T> onConfirmationAction)
            {
                CreationTime = DateTime.Now;
                ConfirmationCode = confirmationCode;
                OnConfirmationAction = onConfirmationAction;
            }
        }

        private readonly int _expirationSpanMinutes;
        private readonly List<ExpirationElement> _expirationElements;
        private readonly Timer _timer;

        public ConfirmationService(int expirationSpanMinutes, int timerPeriod)
        {
            _expirationSpanMinutes = expirationSpanMinutes;
            _expirationElements = new List<ExpirationElement>();
            _timer = new Timer(TimerInvoke, 0, 0, timerPeriod);
        }
        public void Add(E confirmationCode, Action<T> onConfirmationAction)
        {
            _expirationElements.Add(new ExpirationElement(confirmationCode, onConfirmationAction));
        }
        public bool Confirm(E confirmationCode, T into)
        {
            var ex = _expirationElements.FirstOrDefault(e => e.ConfirmationCode.Equals(confirmationCode));
            if(ex != null)
            {
                ex.OnConfirmationAction(into);
                _expirationElements.Remove(ex);
                return true;
            }
            return false;
        }

        private void TimerInvoke(object obj)
        {
            while(0 < _expirationElements.Count)
            {
                if (_expirationElements.First().CreationTime.AddMinutes(_expirationSpanMinutes) < DateTime.Now)
                {
                    _expirationElements.RemoveAt(0);
                }
                else break;
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
