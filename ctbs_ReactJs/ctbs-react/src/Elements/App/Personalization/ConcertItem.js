import { useState } from "react";
import Button from "../../../CommonElements/Button";
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";
import { useLayoutEffect } from "react";
import { Link } from "react-router-dom";

const widthEEm = () => {
    return window.innerWidth / parseFloat(
        getComputedStyle(
            document.querySelector('body')
        )['font-size'])
}
const to = concertId => "/Concerts/" + concertId;

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
    return (<div className="ListItem">
        <Link to={to(concert.concertId)} className="isActiveActionList concertLink">
            <div>
                {concert.isActiveFlag ? "+" : "-"}
            </div>
        </Link>
        <Link to={to(concert.concertId)} className="concertTypeActionList concertLink">
            <div >
                {concert.concertType === 0 ? "Классический" : concert.concertType === 1 ? "Открытое небо" : "Вечеринка"}
            </div>
        </Link>
        <Link to={to(concert.concertId)} className="costActionList concertLink">
            <div >
                {concert.cost}$
            </div>
        </Link>
        <Link to={to(concert.concertId)} className="leftCountActionList concertLink">
            <div >
                {concert.leftCount}
            </div>
        </Link>
        <Link to={to(concert.concertId)}  className="performerActionList concertLink">
            <div>
                {concert.performer.length * 3 > widthEm ? concert.performer.slice(0, widthEm / 4) + "..." : concert.performer}
            </div>
        </Link>
        <Link to={to(concert.concertId)} className="concertDateActionList concertLink">
            <div >
                {new Date(concert.concertDate).toISOString().slice(0, 19).replace(/-/g, "/").replace("T", " ")}
            </div>
        </Link>
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