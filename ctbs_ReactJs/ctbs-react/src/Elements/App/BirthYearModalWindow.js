import store from "../../store";
import * as actionCreators from "../../actionCreators";
import './ModalWindow.css';
import Button from "../../CommonElements/Button";
import DateInput from "../../CommonElements/DateInput";
import { useState } from "react";
import SubmitButton from "../../CommonElements/SubmitButton";

function BirthYearModalWindow() {
    document.querySelector("body").style.overflow = "hidden";
    const [birthDate, setBirthDate] = useState("yyyy-MM-dd");
    return (
        <div className="modal">
            <form id="birthYearModal" className = "Modal" onSubmit={birthYearButtonOnClickFunc(birthDate)}>
                <div className="row-center">
                    <strong className="modalWindowText">Пожалуйста, введите дату вашего рождения</strong>
                    <DateInput value={birthDate} onChange={event => setBirthDate(event.target.value)} min="1900-01-01" max={(new Date()).toISOString().split('T')[0]} />
                </div>
                <div className="row-stretch">
                    <SubmitButton text="Подтвердить возраст" />
                    <Button text="Отказаться вводить" onClick={cancelButtonOnClick} />
                </div>
            </form>
        </div>
    );
}

function birthYearButtonOnClickFunc(birthDate) {
    return function birthYearButtonOnClick() {
        store.dispatch(actionCreators.UpdateBirthDateThunkActionCreator(birthDate));
    }
}
function cancelButtonOnClick() {
    store.dispatch(actionCreators.CancelUpdateBirthDateActionCreator());
}

export default BirthYearModalWindow;