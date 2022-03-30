import * as actionCreators from "../../../actionCreators";
import React, { useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import Button from '../../../CommonElements/Button';
import TextInput from "../../../CommonElements/TextInput";
import DateInput from "../../../CommonElements/DateInput";
import EmailInput from "../../../CommonElements/EmailInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";

function PersonalizationPart(props) {
    const [isActiveEmailModalWindow, setisActiveEmailModalWindow] = useState(false);
    const [userName, setuserName] = useState(store.getState().user.name);
    const [userEmail, setuserEmail] = useState(store.getState().user.email);
    const [userPromoCode, setuserPromoCode] = useState(props.userPromoCode);
    const [birthDate, setBirthDate] = useState(store.getState().user.birthDate !== null && typeof store.getState().user.birthDate !== 'undefined' ? store.getState().user.birthDate.split('T')[0] : "");
    return (
        <div className="centerBox boxColumn">
            <div className="boxRow">
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Id</div>
                    <div className="boxRowRightText">{store.getState().user.userId}</div>
                </div>
                <Button text="Копировать" onClick={copyUserId} />
            </div>
            <form className="boxRow" onSubmit={changeName(userName)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Имя</div>
                    <TextInput value={userName} onChange={event => setuserName(event.target.value)} minLength={3} maxLength={30} />
                </div>
                <SubmitButton text="Изменить" />
            </form>
            <form className="boxRow" onSubmit={birthYearButtonOnClickFunc(birthDate)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Дата рождения</div>
                    <DateInput value={birthDate} onChange={event => setBirthDate(event.target.value)} min="1900-01-01" max={(new Date()).toISOString().split('T')[0]} />
                </div>
                <SubmitButton text="Изменить" />
            </form>
            <form className="boxRow" onSubmit={changeEmail(userEmail, setisActiveEmailModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Email</div>
                    <EmailInput value={userEmail} onChange={event => setuserEmail(event.target.value)} />
                </div>
                <SubmitButton text="Изменить" />
                {isActiveEmailModalWindow && <SimpleModalWindow text="Письмо с подтверждением отправлено на ваш новый email" buttonText="Ok" onClick={closeEmailModalWindow(setisActiveEmailModalWindow)} />}
            </form>
            <form className="boxRow" onSubmit={activatePromoCode(userPromoCode)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Промокод</div>
                    <TextInput value={userPromoCode.uniqueCode} onChange={event => setuserPromoCode(event.target.value)} minLength={3} maxLength={20} />
                    {userPromoCode.discount && <div className="personalizationText">Скидка: {userPromoCode.discount}$</div>}
                </div>
                <SubmitButton text="Активировать" />
            </form>
        </div>
    );
}

function closeEmailModalWindow(setisActiveEmailModalWindow) {
    return function action() {
        setisActiveEmailModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function copyUserId() {
    navigator.clipboard.writeText(store.getState().user.userId);
}
function changeName(name) {
    return function action(event) {
        event.preventDefault();
        if (store.getState().user.name !== name) {
            store.dispatch(actionCreators.UpdateNameActionCreator(name));
        }
    }
}
function activatePromoCode(promoCode) {
    return function action(event) {
        event.preventDefault();
        store.dispatch(actionCreators.ActivatePromoCodeActionCreator(promoCode));
    }
}
function changeEmail(email, setisActiveEmailModalWindow) {
    return function action(event) {
        event.preventDefault();
        if (store.getState().user.email !== email) {
            store.dispatch(actionCreators.UpdateEmailActionCreator(email)).then(() => {
                setisActiveEmailModalWindow(true);
            });
        }
    }
}
function birthYearButtonOnClickFunc(birthDate) {
    return function birthYearButtonOnClick(event) {
        event.preventDefault();
        if (store.getState().user.birthDate !== birthDate) {
            store.dispatch(actionCreators.UpdateBirthDateThunkActionCreator(birthDate));
        }
    }
}

export default PersonalizationPart;