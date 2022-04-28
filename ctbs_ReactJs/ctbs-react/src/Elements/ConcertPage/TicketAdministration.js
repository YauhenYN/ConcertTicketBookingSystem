import { useEffect } from "react";
import { useState } from "react";
import store from "../../store";
import * as actionCreators from "../../actionCreators";
import './ConcertPage.css';
import TicketItem from "./TicketItem";
import NextPageButton from "../../CommonElements/NextPageButton";
import TextInput from "../../CommonElements/TextInput";

function TicketsConcertListSearchInputEvent(concertId, ticketIdSearch, setTickets, setNextPage, setPagesCount) {
    return function Action(event) {
        if (event.key === "Enter") {
            event.preventDefault();
            loadTicketsByConcertId(concertId, ticketIdSearch, [], setTickets, 0, setNextPage, setPagesCount)();
        }
    }
}

function TicketAdministration(props) {
    const [tickets, setTickets] = useState([]);
    const [nextPage, setNextPage] = useState(0);
    const [pagesCount, setPagesCount] = useState(0);
    const [ticketIdSearch, setTicketIdSearch] = useState("");
    const [wereTickets, setWereTickets] = useState(false);
    useEffect(() => {
        loadTicketsByConcertId(props.concertId, null, [], setTickets, 0, setNextPage, setPagesCount)();
    }, [props.concertId]);
    useEffect(() => {
        if (tickets.length > 0) setWereTickets(true);
    }, [ticketIdSearch]);
    return <>
        {(tickets.length > 0 || wereTickets) && <><div className="textHeader">Администрирование</div>
            <div id="TicketListConcertPage" className="outOfListTable">
                <div className="ListTable">
                    <div className="ListHeader">
                        <div className="whoBoughtTicket">Id билета
                            <div id="TicketsConcertListSearchInput" onKeyDown={TicketsConcertListSearchInputEvent(props.concertId, ticketIdSearch, setTickets, setNextPage, setPagesCount)}><TextInput value={ticketIdSearch} onChange={event => setTicketIdSearch(event.target.value)} maxLength={36} /></div>
                        </div>
                        <div className="countTicket">Кол-во</div>
                        <div className="isMarkedTicket">Отмечен</div>
                        <div className="checkTicketButton">
                        </div>
                    </div>
                    <div className="ListItem" /><div className="ListItem" />
                    {tickets.map(ticket => {
                        return <TicketItem key={ticket.ticketId} ticket={ticket} isActive={props.isActive} />
                    })}
                    {nextPage < pagesCount && <NextPageButton onClick={loadTicketsByConcertId(props.concertId, ticketIdSearch, tickets, setTickets, nextPage, setNextPage, setPagesCount)} />}
                </div>
            </div></>}
    </>
}

function loadTicketsByConcertId(concertId, byTicketId, tickets, setTickets, nextPage, setNextPage, setPagesCount) {
    return function action() {
        return store.dispatch(actionCreators.GetManyTicketsActionCreator(nextPage, null, concertId, 15, byTicketId && byTicketId.length > 0 ? byTicketId : null)).then(result => {
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