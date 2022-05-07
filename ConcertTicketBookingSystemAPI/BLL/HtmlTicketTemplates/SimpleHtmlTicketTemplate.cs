using System;

namespace BLL.HtmlTicketTemplates
{
    public class SimpleHtmlTicketTemplate : IHtmlTicketTemplate
    {
        private Guid _ticketId;
        private int _count;
        private string _userName;
        private decimal _cost;
        private string _redirectUrl;
        private int _concertId;
        public SimpleHtmlTicketTemplate(Guid ticketId, int count, string userName, decimal cost, string redirectUrl, int concertId)
        {
            _ticketId = ticketId;
            _count = count;
            _userName = userName;
            _cost = cost;
            _redirectUrl = redirectUrl;
            _concertId = concertId;
        }
        public string GetHtml()
        {
            return $"<p>Id Билета - {_ticketId}</p>" +
                    $"<p>На количество - {_count}</p>" +
                    $"<p>Кому - {_userName}</p>" +
                    $"<p>Оплачено - {_cost}</p>" +
                    $"<h1>На концерт:</h1>" +
                    $"<a href = \"{_redirectUrl}/#/Concerts/{_concertId}\">Концерт</a>";
        }
    }
}
