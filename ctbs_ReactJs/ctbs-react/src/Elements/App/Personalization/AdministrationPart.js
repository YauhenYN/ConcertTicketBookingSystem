import * as actionCreators from "../../../actionCreators";
import React, { useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";

function AdministrationPart() {
    const [isActiveModalWindow, setisActiveModalWindow] = useState(false);
    const [modalWindowText, setModalWindowText] = useState("");
    const [giveRights, setgiveRights] = useState("Id");
    const [takeRights, settakeRights] = useState("Id");
    return (<>
        <div className="administrationBox boxColumn">
            <form className="boxRow" onSubmit={giveAdminRights(giveRights, setisActiveModalWindow, setModalWindowText)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Выдать права администратора</div>
                    <TextInput value={giveRights} onChange={event => setgiveRights(event.target.value)} />
                </div>
                <SubmitButton text="Подтвердить" />
            </form>
            <form className="boxRow" onSubmit={takeAdminRights(takeRights, setisActiveModalWindow, setModalWindowText)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Снять права администратора</div>
                    <TextInput value={takeRights} onChange={event => settakeRights(event.target.value)} />
                </div>
                <SubmitButton text="Подтвердить" />
            </form>
            {isActiveModalWindow && <SimpleModalWindow text={modalWindowText} buttonText="Ok" onClick={closeModalWindow(setisActiveModalWindow)} />}
        </div></>
    );
}

function giveAdminRights(id, setisActiveModalWindow, setModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (id !== "Id") {
            store.dispatch(actionCreators.GiveAdminRightsActionCreator(id)).then(() => {
                setModalWindowText("Права администратора успешно выданы");
                setisActiveModalWindow(true);
            }).catch(() => {
                setModalWindowText("Id не найден / Что-то пошло не так");
                setisActiveModalWindow(true);
            });
        }
    }
}
function takeAdminRights(id, setisActiveModalWindow, setModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (id !== "Id") {
            store.dispatch(actionCreators.TakeAdminRightsActionCreator(id)).then(() => {
                setModalWindowText("Права администратора успешно сняты");
                setisActiveModalWindow(true);
            }).catch(() => {
                setModalWindowText("Id не найден / Что-то пошло не так");
                setisActiveModalWindow(true);
            });
        }
    }
}
function closeModalWindow(setisActiveModalWindow) {
    return function action() {
        setisActiveModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}

export default AdministrationPart;