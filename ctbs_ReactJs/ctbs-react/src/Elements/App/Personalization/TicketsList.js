import "./TicketsPersonalizationList.css";
import { useState } from "react";
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";
import TicketItem from "./TicketItem";
import NextPageButton from "../../../CommonElements/NextPageButton";


function TicketsList(props) {
    const [tickets, setTickets] = useState(props.firstTickets);
    const [nextPage, setNextPage] = useState(1);
    const [pagesCount, setPagesCount] = useState(props.ticketPagesCount);
    return <>
        {tickets.length > 0 && <>
            <div id = "ticketsPersonalizationList" className="outOfListTable">
                <div className="ListTable">
                    <div className="ListHeader">
                        <div className="concertPersonalizationTicket">Исполнитель</div>
                        <div className="countPersonalizationTicket">Количество</div>
                        <div className="isMarkedPersonalizationTicket">Отмечен</div>
                    </div>
                    <div className="ListItem" /><div className="ListItem" />
                    {tickets.map(ticket => {
                        return <TicketItem key={ticket.ticketId} ticket={ticket} />
                    })}
                    {nextPage < pagesCount && <NextPageButton onClick={loadTicketsByConcertId(store.getState().user.userId, tickets, setTickets, nextPage, setNextPage, setPagesCount)} />}
                </div>
            </div></>}
    </>
}

function loadTicketsByConcertId(userId, tickets, setTickets, nextPage, setNextPage, setPagesCount) {
    return function action() {
        store.dispatch(actionCreators.GetManyTicketsActionCreator(nextPage, userId, null, 15)).then(result => {
            setTickets([...tickets, ...result.data.tickets]);
            setPagesCount(result.data.pageCount);
            setNextPage(nextPage + 1);
        })
    }
}

export default TicketsList;