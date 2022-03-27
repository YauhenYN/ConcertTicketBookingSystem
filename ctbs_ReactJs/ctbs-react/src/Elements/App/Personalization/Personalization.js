import ActionList from "./ActionList";
import * as actionCreators from "../../../actionCreators";
import React, { useEffect, useState } from 'react';
import Loading from "../Loading";
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";
import NumberInput from "../../../CommonElements/NumberInput";
import PersonalizationPart from "./PersonalizationPart";

function Personalization() {
    const [actions, setActions] = useState();
    const [isLoading, setisLoading] = useState(true);
    const [isActiveGiveRightsModalWindow, setisActiveGiveRightsModalWindow] = useState(false);
    const [isActiveTakeRightsModalWindow, setisActiveTakeRightsModalWindow] = useState(false);
    const [addPromoCodeModalWindow, setAddPromoCodeModalWindow] = useState(false);
    const [userPromoCode, setuserPromoCode] = useState({});
    const [giveRights, setgiveRights] = useState("Id");
    const [takeRights, settakeRights] = useState("Id");
    const [uniqueCode, setUniqueCode] = useState("");
    const [discount, setDiscount] = useState(0);
    const [onCount, setOnCount] = useState(0);
    useEffect(() => {
        async function dispatches() {
            await store.dispatch(actionCreators.GetActionsActionCreator()).then((result) => {
                setActions(result.data);
            });
            if (store.getState().user.promoCodeId !== null) {
                await store.dispatch(actionCreators.GetPromoCodeByIdThunkActionCreator(store.getState().user.promoCodeId))
                    .then((result) => {
                        if (result.data.isActiveFlag) setuserPromoCode({...result.data});
                    });
            }
            else {
                setuserPromoCode({});
            }
        };
        dispatches().then(() => {
            setisLoading(false);
        });
    }, []);
    return (<>
        {isLoading === true ? (<Loading />) : (<div className="element-common">
            <div className="textHeader">Персонализация</div>
            <PersonalizationPart userPromoCode = {userPromoCode}/>
            {store.getState().user.isAdmin && <>
                <div className="textHeader">Администрирование</div>
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
                </div>
                <div className="centerBox boxColumn">
                    <div className="bigTextCenter">Создание промокода</div>
                    <div className="textAboveBox">
                        <div className="textAboveInput wideLeftAbove">Код</div>
                        <div className="textAboveInput">$</div>
                        <div className="textAboveInput">Кол.</div>
                        <div className="textAboveInput"/>
                    </div>
                    <form className="boxRow" onSubmit={addPromoCode(uniqueCode, discount, onCount, setAddPromoCodeModalWindow)}>
                        <div className="boxRowIn">
                            <div className="wideLeft">
                                <TextInput value={uniqueCode} onChange={event => setUniqueCode(event.target.value)} minLength={3} maxLength={20} />
                            </div>
                            <NumberInput value={discount} onChange={event => setDiscount(event.target.value)} min={0.1} max={100} step=".01" />
                            <NumberInput value={onCount} onChange={event => setOnCount(event.target.value)} min={1} max={500} />
                            {addPromoCodeModalWindow && <SimpleModalWindow text="Промокод создан" buttonText="Ok" onClick={closeAddPromoCodeModalWindow(setAddPromoCodeModalWindow)} />}
                            <SubmitButton text="Создать" />
                        </div>
                    </form>
                </div>

            </>}
            <div className="textHeader">Последние действия</div>
            <ActionList actionList={actions} />
        </div>)
        }</>
    );
}

function addPromoCode(uniqueCode, discount, onCount, setAddPromoCodeModalWindow) {
    return function action(event) {
        event.preventDefault();
        store.dispatch(actionCreators.AddPromoCodeActionCreator(uniqueCode, discount, onCount)).then(() => {
            setAddPromoCodeModalWindow(true);
        });
    }
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
function closeAddPromoCodeModalWindow(addPromoCodeModalWindow) {
    return function action() {
        addPromoCodeModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
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

export default Personalization;