import { Link } from "react-router-dom";

const to = concertId => "/Concerts/" + concertId;

function TicketItem(props) {
    return <div className="ListItem">
        <Link to={to(props.ticket.concertId)} className="concertPersonalizationTicket concertLink">
            <div>
                {props.ticket.concertPerformer}
            </div>
        </Link>
        <div className="concertPersonalizationTicket">
            {props.ticket.ticketId}
        </div>
        <div className="countPersonalizationTicket">
            {props.ticket.onCount}
        </div>
        <div className="isMarkedPersonalizationTicket">
            {props.ticket.isMarked ? "Да" : "Нет"}
        </div>
    </div >
}

export default TicketItem;