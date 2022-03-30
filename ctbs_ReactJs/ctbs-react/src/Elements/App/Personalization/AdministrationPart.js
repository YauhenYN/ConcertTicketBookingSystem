import * as actionCreators from "../../../actionCreators";
import React, { useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";

function AdministrationPart() {
    const [isActiveGiveRightsModalWindow, setisActiveGiveRightsModalWindow] = useState(false);
    const [isActiveTakeRightsModalWindow, setisActiveTakeRightsModalWindow] = useState(false);
    const [giveRights, setgiveRights] = useState("Id");
    const [takeRights, settakeRights] = useState("Id");
    return (<>
        <div className="administrationBox boxColumn">
            <form className="boxRow" onSubmit={giveAdminRights(giveRights, setisActiveGiveRightsModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Выдать права администратора</div>
                    <TextInput value={giveRights} onChange={event => setgiveRights(event.target.value)} />
                </div>
                <SubmitButton text="Подтвердить" />
                {isActiveGiveRightsModalWindow && <SimpleModalWindow text="Права администратора выданы успешно" buttonText="Ok" onClick={closeGiveRightsModalWindow(setisActiveGiveRightsModalWindow)} />}
            </form>
            <form className="boxRow" onSubmit={takeAdminRights(takeRights, setisActiveTakeRightsModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Снять права администратора</div>
                    <TextInput value={takeRights} onChange={event => settakeRights(event.target.value)} />
                </div>
                <SubmitButton text="Подтвердить" />
                {isActiveTakeRightsModalWindow && <SimpleModalWindow text="Права администратора сняты успешно" buttonText="Ok" onClick={closeTakeRightsModalWindow(setisActiveTakeRightsModalWindow)} />}
            </form>
        </div></>
    );
}

function giveAdminRights(id, setisActiveGiveRightsModalWindow) {
    return function action(event) {
        event.preventDefault();
        if (id !== "Id") {
            store.dispatch(actionCreators.GiveAdminRightsActionCreator(id)).then(() => {
                setisActiveGiveRightsModalWindow(true);
            });
        }
    }
}
function takeAdminRights(id, setisActiveTakeRightsModalWindow) {
    return function action(event) {
        event.preventDefault();
        if (id !== "Id") {
            store.dispatch(actionCreators.TakeAdminRightsActionCreator(id)).then(() => {
                setisActiveTakeRightsModalWindow(true);
            });
        }
    }
}
function closeGiveRightsModalWindow(setisActiveGiveRightsModalWindow) {
    return function action() {
        setisActiveGiveRightsModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function closeTakeRightsModalWindow(setisActiveTakeRightsModalWindow) {
    return function action() {
        setisActiveTakeRightsModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}

export default AdministrationPart;