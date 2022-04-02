import { useState } from "react";
import Button from "../../../CommonElements/Button";
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";
import { useLayoutEffect } from "react";

const widthEEm = () => {
    return window.innerWidth / parseFloat(
        getComputedStyle(
            document.querySelector('body')
        )['font-size'])
}

function ConcertItem(props) {
    const [widthEm, setWidthEm] = useState(widthEEm());
    const [concert, setConcert] = useState(props.concert);
    useLayoutEffect(() => {
        function settingWidthEm() {
            setWidthEm(widthEEm());
        }
        window.addEventListener('resize', settingWidthEm);
        return () => window.removeEventListener('resize', settingWidthEm);
    }, []);
    return (
        <div className="ListItem">
            <div className="isActiveActionList">
                {concert.isActiveFlag ? "+" : "-"}
            </div>
            <div className="concertTypeActionList">
                {concert.concertType === 0 ? "Классический" : concert.concertType === 1 ? "Открытое небо" : "Вечеринка"}
            </div>
            <div className="costActionList">
                {concert.cost}$
            </div>
            <div className="leftCountActionList">
                {concert.leftCount}
            </div>
            <div className="performerActionList">
                {concert.performer.length * 3 > widthEm ? concert.performer.slice(0, widthEm / 4) + "..." : concert.performer}
            </div>
            <div className="concertDateActionList">
                {concert.concertDate}
            </div>
            <div className="activateButtonActionList">
                {concert.isActiveFlag ? <Button text="Деактивировать" onClick={deActivateConcert(concert, setConcert)} /> : <Button text="Активировать" onClick={activateConcert(concert, setConcert)} />}
            </div>
        </div>
    );
}

function activateConcert(concert, setConcert) {
    return function action() {
        store.dispatch(actionCreators.ActivateConcertInListActionCreator(concert.concertId)).then(() => {
            setConcert({
                ...concert,
                isActiveFlag: true
            })
        });
    }
}
function deActivateConcert(concert, setConcert) {
    return function action() {
        store.dispatch(actionCreators.DeactivateConcertInListActionCreator(concert.concertId)).then(() => {
            setConcert({
                ...concert,
                isActiveFlag: false
            })
        });
    }
}
export default ConcertItem;