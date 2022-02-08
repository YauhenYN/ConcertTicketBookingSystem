using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService
{
    public class GuidConfirmationService : IConfirmationService<Guid>, IDisposable
    {
        private class ExpirationElement
        {
            public DateTime CreationTime { get; }
            public Guid ConfirmationCode { get; }
            public Action OnConfirmationAction { get; }
            public ExpirationElement(Guid confirmationCode, Action onConfirmationAction)
            {
                CreationTime = DateTime.Now;
                ConfirmationCode = confirmationCode;
                OnConfirmationAction = onConfirmationAction;
            }
        }

        private readonly TimeSpan _expirationSpan;
        private readonly List<ExpirationElement> _expirationElements;
        private readonly Timer _timer;
        public GuidConfirmationService(TimeSpan exprirationSpan, int timerPeriod)
        {
            _expirationSpan = exprirationSpan;
            _expirationElements = new List<ExpirationElement>();
            _timer = new Timer(TimerInvoke, 0, 0, timerPeriod);
        }
        public void Add(Guid confirmationCode, Action onConfirmationAction)
        {
            _expirationElements.Add(new ExpirationElement(confirmationCode, onConfirmationAction));
        }
        public bool Confirm(Guid confirmationCode)
        {
            var ex = _expirationElements.FindLast(e => e.ConfirmationCode == confirmationCode);
            if(ex != null)
            {
                ex.OnConfirmationAction();
                _expirationElements.Remove(ex);
                return true;
            }
            return false;
        }

        private void TimerInvoke(object obj)
        {
            while(0 < _expirationElements.Count)
            {
                if (_expirationElements.First().CreationTime + _expirationSpan < DateTime.Now)
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
