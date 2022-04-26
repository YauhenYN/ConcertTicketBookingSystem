import * as actionCreators from "../../../actionCreators";
import React, { useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";
import NumberInput from "../../../CommonElements/NumberInput";

function CreatePromoCodePart() {
    const [addPromoCodeModalWindow, setAddPromoCodeModalWindow] = useState(false);
    const [noSuchPromoCodeModalWindow, setNoSuchPromoCodeModalWindow] = useState(false);
    const [uniqueCode, setUniqueCode] = useState("");
    const [discount, setDiscount] = useState(0);
    const [onCount, setOnCount] = useState(0);
    return (
        <div className="centerBox boxColumn">
            <div className="bigTextCenter">Создание промокода</div>
            <div className="textAboveBox">
                <div className="textAboveInput wideLeftAbove">Код</div>
                <div className="textAboveInput">$</div>
                <div className="textAboveInput">Кол.</div>
                <div className="textAboveInput" />
            </div>
            <form className="boxRow" onSubmit={addPromoCode(uniqueCode, discount, onCount, setAddPromoCodeModalWindow, setNoSuchPromoCodeModalWindow)}>
                <div className="boxRowIn">
                    <div className="wideLeft">
                        <TextInput value={uniqueCode} onChange={event => setUniqueCode(event.target.value)} minLength={3} maxLength={20} />
                    </div>
                    <NumberInput value={discount} onChange={event => setDiscount(event.target.value)} min={0.1} max={500 / onCount} step=".01" />
                    <NumberInput value={onCount} onChange={event => setOnCount(event.target.value)} min={1} max={500 / discount} />
                    {addPromoCodeModalWindow && <SimpleModalWindow text="Промокод создан" buttonText="Ok" onClick={closeModalWindow(setAddPromoCodeModalWindow)} />}
                    {noSuchPromoCodeModalWindow && <SimpleModalWindow text="Промокод с данным названием уже существует / Что-то пошло не так" buttonText="Ok" onClick={closeModalWindow(setNoSuchPromoCodeModalWindow)} />}
                    <SubmitButton text="Создать" />
                </div>
            </form>
        </div>
    );
}

function addPromoCode(uniqueCode, discount, onCount, setAddPromoCodeModalWindow, setNoSuchPromoCodeModalWindow) {
    return function action(event) {
        event.preventDefault();
        store.dispatch(actionCreators.AddPromoCodeActionCreator(uniqueCode, discount, onCount)).then(() => {
            setAddPromoCodeModalWindow(true);
        }).catch(() => {
            setNoSuchPromoCodeModalWindow(true);
        });
    }
}

function closeModalWindow(setModalWindow) {
    return function action() {
        setModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}

export default CreatePromoCodePart;