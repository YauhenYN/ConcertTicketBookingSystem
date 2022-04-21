
import store from "../../store";
import * as actionCreators from "../../actionCreators";
import { useState } from "react";
import Button from '../../CommonElements/Button';


function TicketItem(props) {
    const [ticket, setTicket] = useState(props.ticket);

    return <div className="ListItem">
        <div className="whoBoughtTicket">
            {ticket.ticketId}
        </div>
        <div className="countTicket">
            {ticket.onCount}
        </div>
        <div className="isMarkedTicket">
            {ticket.isMarked ? "Да" : "Нет"}
        </div>
        <div className="checkTicketButton">
        {ticket.isMarked ? <Button text="Деактивировать" onClick={UnMarkTicket(ticket, setTicket)} /> : <Button text="Отметить" onClick={MarkTicket(ticket, setTicket)} />}
        </div>
    </div>
}

function MarkTicket(ticket, setTicket){
    return function action(){
        store.dispatch(actionCreators.MarkTicketActionCreator(ticket.ticketId)).then(result => {
            setTicket({...ticket, isMarked: true});
        })
    }
}

function UnMarkTicket(ticket, setTicket){
    return function action(){
        store.dispatch(actionCreators.UnmarkTicketActionCreator(ticket.ticketId)).then(result => {
            setTicket({...ticket, isMarked: false});
        })
    }
}

export default TicketItem;