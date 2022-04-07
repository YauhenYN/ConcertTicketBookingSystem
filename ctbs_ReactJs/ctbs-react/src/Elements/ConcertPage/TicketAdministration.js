import { useEffect } from "react";
import { useState } from "react";
import store from "../../store";
import * as actionCreators from "../../actionCreators";
import './ConcertPage.css';
import TicketItem from "./TicketItem";
import NextPageButton from "../../CommonElements/NextPageButton";


function TicketAdministration(props) {
    const [tickets, setTickets] = useState([]);
    const [nextPage, setNextPage] = useState(0);
    const [pagesCount, setPagesCount] = useState(0);
    useEffect(() => {
        loadTicketsByConcertId(props.concertId, [], setTickets, 0, setNextPage, setPagesCount)();
    }, [props])
    return <>
        {tickets.length > 0 && <><div className="textHeader">Администрирование</div>
            <div id = "TicketListConcertPage" className="outOfListTable">
                <div className="ListTable">
                    <div className="ListHeader">
                        <div className="whoBoughtTicket">Кто купил</div>
                        <div className="countTicket">Количество</div>
                        <div className="isMarkedTicket">Отмечен</div>
                        <div className="checkTicketButton"></div>
                    </div>
                    <div className="ListItem" /><div className="ListItem" />
                    {tickets.map(ticket => {
                        return <TicketItem key={ticket.ticketId} ticket={ticket} />
                    })}
                    {nextPage < pagesCount && <NextPageButton onClick={loadTicketsByConcertId(props.concertId, tickets, setTickets, nextPage, setNextPage, setPagesCount)} />}
                </div>
            </div></>}
    </>
}

function loadTicketsByConcertId(concertId, tickets, setTickets, nextPage, setNextPage, setPagesCount) {
    return function action() {
        store.dispatch(actionCreators.GetManyTicketsActionCreator(nextPage, null, concertId, 15)).then(result => {
            setTickets([...tickets, ...result.data.tickets]);
            setPagesCount(result.data.pageCount);
            setNextPage(nextPage + 1);
        })
    }
}

export default TicketAdministration;