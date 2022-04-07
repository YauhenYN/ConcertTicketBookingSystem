import * as actionCreators from "../../../actionCreators";
import React, { useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";

function TicketAdministrationPart() {
    const [isActiveMarkTicketModalWindow, setisActiveMarkTicketModalWindow] = useState(false);
    const [isActiveUnmarkTicketModalWindow, setisActiveUnmarkTicketModalWindow] = useState(false);
    const [markTicket, setMarkTicket] = useState("Id билета");
    const [unmarkTicket, setUnmarkTicket] = useState("Id билета");
    return (<>
        <div className="administrationBox boxColumn">
            <form className="boxRow" onSubmit={MarkTicket(markTicket, setisActiveMarkTicketModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Отметить билет</div>
                    <TextInput value={markTicket} onChange={event => setMarkTicket(event.target.value)} />
                </div>
                <SubmitButton text="Подтвердить" />
                {isActiveMarkTicketModalWindow && <SimpleModalWindow text="Билет отмечен успешно" buttonText="Ok" onClick={closeMarkTicketModalWindow(setisActiveMarkTicketModalWindow)} />}
            </form>
            <form className="boxRow" onSubmit={UnmarkTicket(unmarkTicket, setisActiveUnmarkTicketModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Снять отметку с билета</div>
                    <TextInput value={unmarkTicket} onChange={event => setUnmarkTicket(event.target.value)} />
                </div>
                <SubmitButton text="Подтвердить" />
                {isActiveUnmarkTicketModalWindow && <SimpleModalWindow text="С билета снята отметка успешно" buttonText="Ok" onClick={closeUnmarkTicketModalWindow(setisActiveUnmarkTicketModalWindow)} />}
            </form>
        </div></>
    );
}

function MarkTicket(id, setisActiveMarkTicketModalWindow) {
    return function action(event) {
        event.preventDefault();
        if (id !== "Id") {
            store.dispatch(actionCreators.MarkTicketActionCreator(id)).then(() => {
                setisActiveMarkTicketModalWindow(true);
            });
        }
    }
}
function UnmarkTicket(id, setisActiveUnmarkTicketModalWindow) {
    return function action(event) {
        event.preventDefault();
        if (id !== "Id") {
            store.dispatch(actionCreators.UnmarkTicketActionCreator(id)).then(() => {
                setisActiveUnmarkTicketModalWindow(true);
            });
        }
    }
}
function closeMarkTicketModalWindow(setisActiveMarkTicketModalWindow) {
    return function action() {
        setisActiveMarkTicketModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function closeUnmarkTicketModalWindow(setisActiveUnmarkTicketModalWindow) {
    return function action() {
        setisActiveUnmarkTicketModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}

export default TicketAdministrationPart;