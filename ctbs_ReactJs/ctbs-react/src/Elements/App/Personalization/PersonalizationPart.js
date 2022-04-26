import * as actionCreators from "../../../actionCreators";
import React, { useEffect, useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import Button from '../../../CommonElements/Button';
import TextInput from "../../../CommonElements/TextInput";
import DateInput from "../../../CommonElements/DateInput";
import EmailInput from "../../../CommonElements/EmailInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";

function PersonalizationPart(props) {
    const [isModalWindow, setisModalWindow] = useState(false);
    const [modalWindowText, setisModalWindowText] = useState();
    const [userName, setuserName] = useState(store.getState().user.name);
    const [userEmail, setuserEmail] = useState(store.getState().user.email);
    const [userPromoCode, setuserPromoCode] = useState(props.userPromoCode);
    const [firstPromoCodeName, setFirstPromoCodeName] = useState("");
    const [birthDate, setBirthDate] = useState(store.getState().user.birthDate !== null && typeof store.getState().user.birthDate !== 'undefined' ? store.getState().user.birthDate.split('T')[0] : "");
    const userPromoCodeId = store.getState().user.promoCodeId;
    useEffect(() => {
        if (userPromoCodeId !== null) {
            store.dispatch(actionCreators.GetPromoCodeByIdThunkActionCreator(userPromoCodeId))
                .then((result) => {
                    if (result.data.isActiveFlag) {
                        setuserPromoCode({ ...result.data });
                        setFirstPromoCodeName(result.data.uniqueCode);
                    }
                }).catch(() => { });
        }
        else {
            setuserPromoCode({});
        }
    }, [userPromoCodeId]);
    console.log();
    return (
        <div className="centerBox boxColumn">
            <div className="boxRow">
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Id</div>
                    <div className="boxRowRightText">{store.getState().user.userId}</div>
                </div>
                <Button text="Копировать" onClick={copyUserId(setisModalWindow, setisModalWindowText)} />
            </div>
            <form className="boxRow" onSubmit={changeName(userName,  setisModalWindow, setisModalWindowText)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Имя</div>
                    <TextInput value={userName} onChange={event => setuserName(event.target.value)} minLength={3} maxLength={30} />
                </div>
                <SubmitButton text="Изменить" />
            </form>
            <form className="boxRow" onSubmit={birthYearButtonOnClickFunc(birthDate,  setisModalWindow, setisModalWindowText)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Дата рождения</div>
                    <DateInput value={birthDate} onChange={event => setBirthDate(event.target.value)} min="1900-01-01" max={(new Date()).toISOString().split('T')[0]} />
                </div>
                <SubmitButton text="Изменить" />
            </form>
            <form className="boxRow" onSubmit={changeEmail(userEmail,  setisModalWindow, setisModalWindowText)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Email</div>
                    <EmailInput value={userEmail} onChange={event => setuserEmail(event.target.value)} />
                </div>
                <SubmitButton text="Изменить" />
            </form>
            <form className="boxRow" onSubmit={activatePromoCode(firstPromoCodeName, userPromoCode.uniqueCode, setisModalWindow, setisModalWindowText)}>
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Промокод</div>
                    <TextInput value={userPromoCode.uniqueCode ? userPromoCode.uniqueCode : ""} onChange={event => setuserPromoCode({ ...userPromoCode, uniqueCode: event.target.value })} minLength={3} maxLength={20} />
                    {userPromoCode.discount && <div className="personalizationText">Скидка: {userPromoCode.discount}$</div>}
                </div>
                <SubmitButton text="Активировать" />
            </form>
            {isModalWindow && <SimpleModalWindow text={modalWindowText} buttonText="Ok" onClick={modalWindowAction(setisModalWindow)} />}
        </div>
    );
}
function modalWindowAction(act) {
    return function action() {
        act(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function copyUserId(setisModalWindow, setisModalWindowText) {
    return function action() {
        setisModalWindowText("Id успешно скопирован");
        setisModalWindow(true);
        navigator.clipboard.writeText(store.getState().user.userId);
    }
}
function changeName(name, setisModalWindow, setisModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (store.getState().user.name !== name) {
            store.dispatch(actionCreators.UpdateNameActionCreator(name)).then(() => {
                setisModalWindowText("Имя успешно изменено");
                setisModalWindow(true);
            }).catch(() => {
                setisModalWindowText("Что-то пошло не так");
                setisModalWindow(true);
            });
        }
        else {
            setisModalWindowText("Невозможно изменить имя на текущее");
            setisModalWindow(true);
        }
    }
}
function activatePromoCode(firstPromoCodeName, promoCode, setisModalWindow, setisModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (promoCode !== firstPromoCodeName) {
            store.dispatch(actionCreators.ActivatePromoCodeActionCreator(promoCode)).then(() => {
                setisModalWindowText("Промокод успешно активирован");
                setisModalWindow(true);
                store.dispatch(actionCreators.GetUserInfoThunkActionCreator());
            }).catch(() => {
                setisModalWindowText("Действительный промокод не найден / Действующий промокод лучше");
                setisModalWindow(true);
            });
        }
        else{
            setisModalWindowText("Невозможно активировать уже действующий промокод");
            setisModalWindow(true);
        }
    }
}
function changeEmail(email, setisModalWindow, setisModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (store.getState().user.email !== email) {
            store.dispatch(actionCreators.UpdateEmailActionCreator(email)).then(() => {
                setisModalWindowText("Письмо с подтверждением отправлено на ваш email");
                setisModalWindow(true);
            }).catch(() => {
                setisModalWindowText("Email адрес не действителен");
                setisModalWindow(true);
            });
        }
        else {
            setisModalWindowText("Невозможно изменить email на текущий");
            setisModalWindow(true);
        }
    }
}
function birthYearButtonOnClickFunc(birthDate, setisModalWindow, setisModalWindowText) {
    return function birthYearButtonOnClick(event) {
        event.preventDefault();
        if (store.getState().user.birthDate !== birthDate) {
            store.dispatch(actionCreators.UpdateBirthDateThunkActionCreator(birthDate)).then(() => {
                setisModalWindowText("Дата рождения успешно изменена");
                setisModalWindow(true);
            }).catch(() => {
                setisModalWindowText("Что-то пошло не так");
                setisModalWindow(true);
            });
        }
        else{
            setisModalWindowText("Невозможно изменить дату рождения на текущую");
            setisModalWindow(true);
        }
    }
}

export default PersonalizationPart;