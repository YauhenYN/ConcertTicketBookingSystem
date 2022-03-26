import ActionList from "./ActionList";
import * as actionCreators from "../../../actionCreators";
import React, { useEffect, useState } from 'react';
import Loading from "../Loading";
import store from "../../../store";
import './Personalization.css';
import Button from '../../../CommonElements/Button';
import TextInput from "../../../CommonElements/TextInput";
import DateInput from "../../../CommonElements/DateInput";
import EmailInput from "../../../CommonElements/EmailInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";

function Personalization() {
    const [actions, setActions] = useState();
    const [isLoading, setisLoading] = useState(true);
    const [isActiveEmailModalWindow, setisActiveEmailModalWindow] = useState(false);
    const [isActiveGiveRightsModalWindow, setisActiveGiveRightsModalWindow] = useState(false);
    const [userName, setuserName] = useState(store.getState().user.name);
    const [userEmail, setuserEmail] = useState(store.getState().user.email);
    const [userPromoCode, setuserPromoCode] = useState();
    const [giveRights, setgiveRights] = useState("Id");
    const [birthDate, setBirthDate] = useState(store.getState().user.birthDate !== null && typeof store.getState().user.birthDate !== 'undefined' ? store.getState().user.birthDate.split('T')[0] : "");
    useEffect(() => {
        async function dispatches() {
            await store.dispatch(actionCreators.GetActionsActionCreator()).then((result) => {
                setActions(result.data);
            });
            if (store.getState().user.promoCodeId !== null) {
                await store.dispatch(actionCreators.GetPromoCodeByIdThunkActionCreator(store.getState().user.promoCodeId))
                    .then((result) => {
                        if (result.isActiveFlag) setuserPromoCode(result.data);
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
            <div className="centerBox boxColumn">
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Id</div>
                        <div className="boxRowRightText">{store.getState().user.userId}</div>
                    </div>
                    <Button text="Копировать" onClick={copyUserId} />
                </div>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Имя</div>
                        <TextInput value={userName} onChange={event => setuserName(event.target.value)} minLength={3} maxLength={30} />
                    </div>
                    <Button text="Изменить" onClick={changeName(userName)} />
                </div>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Дата рождения</div>
                        <DateInput value={birthDate} onChange={event => setBirthDate(event.target.value)} min="1900-01-01" max={(new Date()).toISOString().split('T')[0]} />
                    </div>
                    <Button text="Изменить" onClick={birthYearButtonOnClickFunc(birthDate)} />
                </div>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Email</div>
                        <EmailInput value={userEmail} onChange={event => setuserEmail(event.target.value)} />
                    </div>
                    <Button text="Изменить" onClick={changeEmail(userEmail, setisActiveEmailModalWindow)} />
                    {isActiveEmailModalWindow && <SimpleModalWindow text="Письмо с подтверждением отправлено на ваш новый email" buttonText="Ok" onClick={closeEmailModalWindow(setisActiveEmailModalWindow)} />}
                </div>
                <form className="boxRow" onSubmit={activatePromoCode(userPromoCode)}>
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Промокод</div>
                        <TextInput value={userPromoCode.uniqueCode} onChange={event => setuserPromoCode(event.target.value)} minLength={5} maxLength={20} />
                        {userPromoCode.discount && <div className="personalizationText">Discount: {userPromoCode.discount}</div>}
                    </div>
                    <SubmitButton text="Активировать" />
                </form>
            </div>
            {store.getState().user.isAdmin && <>
                <div className="textHeader">Администрирование</div>
                <div className="administrationBox boxColumn">
                    <div className="boxRow">
                        <div className="boxRowIn">
                            <div className="boxRowLeftText">Выдать права администратора</div>
                            <TextInput value={giveRights} onChange={event => setgiveRights(event.target.value)} />
                        </div>
                        <Button text="Подтвердить" onClick={giveAdminRights(giveRights, setisActiveGiveRightsModalWindow)} />
                    </div>
                    {isActiveGiveRightsModalWindow && <SimpleModalWindow text="Права администратора выданы успешно" buttonText="Ok" onClick={closeGiveRightsModalWindow(setisActiveGiveRightsModalWindow)} />}
                </div>
            </>}
            <div className="textHeader">Последние действия</div>
            <ActionList actionList={actions} />
        </div>)}</>
    );
}

function giveAdminRights(id, setisActiveGiveRightsModalWindow) {

    return function action() {
        if (id !== "Id") {
            store.dispatch(actionCreators.GiveAdminRightsActionCreator(id)).then(() => {
                setisActiveGiveRightsModalWindow(true);
            });
        }
    }
}

function closeEmailModalWindow(setisActiveEmailModalWindow) {
    return function action() {
        setisActiveEmailModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function closeGiveRightsModalWindow(setisActiveGiveRightsModalWindow) {
    return function action() {
        setisActiveGiveRightsModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function copyUserId() {
    navigator.clipboard.writeText(store.getState().user.userId);
}
function changeName(name) {
    return function action() {
        if (store.getState().user.name !== name) {
            store.dispatch(actionCreators.UpdateNameActionCreator(name));
        }
    }
}
function activatePromoCode(promoCode) {
    return function action() {
        store.dispatch(actionCreators.ActivatePromoCodeActionCreator(promoCode));
    }
}
function changeEmail(email, setisActiveEmailModalWindow) {
    return function action() {
        if (store.getState().user.email !== email) {
            store.dispatch(actionCreators.UpdateEmailActionCreator(email)).then(() => {
                setisActiveEmailModalWindow(true);
            });
        }
    }
}
function birthYearButtonOnClickFunc(birthDate) {
    return function birthYearButtonOnClick() {
        if (store.getState().user.birthDate !== birthDate) {
            store.dispatch(actionCreators.UpdateBirthDateThunkActionCreator(birthDate));
        }
    }
}

export default Personalization;