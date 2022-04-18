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
    const [NoSuchEmailModalWindow, setNoSuchEmailModalWindow] = useState(false);
    const [isCopiedIdModalWindow, setIsCopiedIdModalWindow] = useState(false);
    const [isChangesNameModalWindow, setIsChangedNameModalWindow] = useState(false);
    const [isChangedBirthDateModalWindow, setIsChangedBirthDateModalWindow] = useState(false);
    const [isActivatedPromoCodeModalWindow, setIsActivatedPromoCodeModalWindow] = useState(false);
    const [noSuchPromoCodeModalWindow, setNoSuchPromoCodeModalWindow] = useState(false);
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
                <Button text="Копировать" onClick={copyUserId(setIsCopiedIdModalWindow)} />
                {isCopiedIdModalWindow && <SimpleModalWindow text="Id скопирован" buttonText="Ok" onClick={modalWindowAction(setIsCopiedIdModalWindow)} />}
            </div>
            <form className="boxRow" onSubmit={changeName(userName, setIsChangedNameModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Имя</div>
                    <TextInput value={userName} onChange={event => setuserName(event.target.value)} minLength={3} maxLength={30} />
                </div>
                <SubmitButton text="Изменить" />
                {isChangesNameModalWindow && <SimpleModalWindow text="Имя успешно изменено" buttonText="Ok" onClick={modalWindowAction(setIsChangedNameModalWindow)} />}
            </form>
            <form className="boxRow" onSubmit={birthYearButtonOnClickFunc(birthDate, setIsChangedBirthDateModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Дата рождения</div>
                    <DateInput value={birthDate} onChange={event => setBirthDate(event.target.value)} min="1900-01-01" max={(new Date()).toISOString().split('T')[0]} />
                </div>
                <SubmitButton text="Изменить" />
                {isChangedBirthDateModalWindow && <SimpleModalWindow text="Дата рождения успешно изменена" buttonText="Ok" onClick={modalWindowAction(setIsChangedBirthDateModalWindow)} />}
            </form>
            <form className="boxRow" onSubmit={changeEmail(userEmail, setisActiveEmailModalWindow, setNoSuchEmailModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Email</div>
                    <EmailInput value={userEmail} onChange={event => setuserEmail(event.target.value)} />
                </div>
                <SubmitButton text="Изменить" />
                {isActiveEmailModalWindow && <SimpleModalWindow text="Письмо с подтверждением отправлено на ваш новый email" buttonText="Ok" onClick={modalWindowAction(setisActiveEmailModalWindow)} />}
                {NoSuchEmailModalWindow && <SimpleModalWindow text="Не удалось отправить письмо на указанный email" buttonText="Ok" onClick={modalWindowAction(setNoSuchEmailModalWindow)} />}
            </form>
            <form className="boxRow" onSubmit={activatePromoCode(userPromoCode, setIsActivatedPromoCodeModalWindow, setNoSuchPromoCodeModalWindow)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Промокод</div>
                    <TextInput value={userPromoCode.uniqueCode} onChange={event => setuserPromoCode({...userPromoCode, uniqueCode: event.target.value})} minLength={3} maxLength={20} />
                    {userPromoCode.discount && <div className="personalizationText">Скидка: {userPromoCode.discount}$</div>}
                </div>
                <SubmitButton text="Активировать" />
                {isActivatedPromoCodeModalWindow && <SimpleModalWindow text="Промокод успешно активирован" buttonText="Ok" onClick={modalWindowAction(setIsActivatedPromoCodeModalWindow)} />}
                {noSuchPromoCodeModalWindow && <SimpleModalWindow text="Промокода с таким названием не существует" buttonText="Ok" onClick={modalWindowAction(setNoSuchPromoCodeModalWindow)} />}
            </form>
        </div>
    );
}
function modalWindowAction(act) {
    return function action() {
        act(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function copyUserId(setIsCopiedIdModalWindow) {
    return function action() {
        setIsCopiedIdModalWindow(true);
        navigator.clipboard.writeText(store.getState().user.userId);
    }
}
function changeName(name, setIsChangedNameModalWindow) {
    return function action(event) {
        event.preventDefault();
        if (store.getState().user.name !== name) {
            store.dispatch(actionCreators.UpdateNameActionCreator(name)).then(() => {
                setIsChangedNameModalWindow(true);
            });
        }
    }
}
function activatePromoCode(promoCode, setIsActivatedPromoCodeModalWindow, setNoSuchPromoCodeModalWindow) {
    return function action(event) {
        event.preventDefault();
        store.dispatch(actionCreators.ActivatePromoCodeActionCreator(promoCode)).then(() => {
            setIsActivatedPromoCodeModalWindow(true);
        }).catch(() => {
            setNoSuchPromoCodeModalWindow(true);
        });
    }
}
function changeEmail(email, setisActiveEmailModalWindow, setNoSuchEmailModalWindow) {
    return function action(event) {
        event.preventDefault();
        if (store.getState().user.email !== email) {
            store.dispatch(actionCreators.UpdateEmailActionCreator(email)).then(() => {
                setisActiveEmailModalWindow(true);
            }).catch(() => {
                setNoSuchEmailModalWindow(true);
            });
        }
    }
}
function birthYearButtonOnClickFunc(birthDate, setIsChangedBirthDateModalWindow) {
    return function birthYearButtonOnClick(event) {
        event.preventDefault();
        if (store.getState().user.birthDate !== birthDate) {
            store.dispatch(actionCreators.UpdateBirthDateThunkActionCreator(birthDate)).then(() => {
                setIsChangedBirthDateModalWindow(true);
            });
        }
    }
}

export default PersonalizationPart;