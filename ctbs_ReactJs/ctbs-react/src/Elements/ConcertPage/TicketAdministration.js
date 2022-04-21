import { useEffect } from "react";
import { useState } from "react";
import store from "../../store";
import * as actionCreators from "../../actionCreators";
import './ConcertPage.css';
import TicketItem from "./TicketItem";
import NextPageButton from "../../CommonElements/NextPageButton";
import TextInput from "../../CommonElements/TextInput";


function TicketAdministration(props) {
    const [tickets, setTickets] = useState([]);
    const [nextPage, setNextPage] = useState(0);
    const [pagesCount, setPagesCount] = useState(0);
    const [ticketIdSearch, setTicketIdSearch] = useState("");
    useEffect(() => {
        loadTicketsByConcertId(props.concertId, ticketIdSearch, [], setTickets, 0, setNextPage, setPagesCount)();
    }, [props.concertId, ticketIdSearch]);
    return <>
        {(tickets.length > 0 || ticketIdSearch.length >= 3) && <><div className="textHeader">Администрирование</div>
            <div id="TicketListConcertPage" className="outOfListTable">
                <div className="ListTable">
                    <div className="ListHeader">
                        <div className="whoBoughtTicket">Id билета
                            <TextInput value={ticketIdSearch} onChange={event => setTicketIdSearch(event.target.value)} minLength={3} maxLength={36} />
                        </div>
                        <div className="countTicket">Кол-во</div>
                        <div className="isMarkedTicket">Отмечен</div>
                        <div className="checkTicketButton">
                        </div>
                    </div>
                    <div className="ListItem" /><div className="ListItem" />
                    {tickets.map(ticket => {
                        return <TicketItem key={ticket.ticketId} ticket={ticket} />
                    })}
                    {nextPage < pagesCount && <NextPageButton onClick={loadTicketsByConcertId(props.concertId, ticketIdSearch, tickets, setTickets, nextPage, setNextPage, setPagesCount)} />}
                </div>
            </div></>}
    </>
}

function loadTicketsByConcertId(concertId, byTicketId, tickets, setTickets, nextPage, setNextPage, setPagesCount) {
    return function action() {
        return store.dispatch(actionCreators.GetManyTicketsActionCreator(nextPage, null, concertId, 15, byTicketId && byTicketId.length >= 3 ? byTicketId : null)).then(result => {
            setTickets([...tickets, ...result.data.tickets]);
            setPagesCount(result.data.pageCount);
            setNextPage(nextPage + 1);
        }).catch(() => {
            setTickets([]);
            setPagesCount(0);
            setNextPage(0);
        });
    }
}

export default TicketAdministration;