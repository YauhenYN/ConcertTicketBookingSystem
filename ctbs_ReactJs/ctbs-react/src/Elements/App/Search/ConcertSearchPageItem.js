import { useLayoutEffect } from "react";
import { useState } from "react";
import { Link } from "react-router-dom";
import { apiLink } from "../../../configuration";
import './ConcertSearchPageItem.css';

const widthEEm = () => {
    let em = window.innerWidth / parseFloat(
        getComputedStyle(
            document.querySelector('body')
        )['font-size']);
    return em > 50 ? em : 50
}

function ConcertSearchPageItem(props) {
    const [widthEm, setWidthEm] = useState(widthEEm());
    useLayoutEffect(() => {
        function settingWidthEm() {
            setWidthEm(widthEEm());
        }
        window.addEventListener('resize', settingWidthEm);
        return () => window.removeEventListener('resize', settingWidthEm);
    }, []);
    return (
        <Link className="searchPageItem" to={"/Concerts/" + props.concert.concertId}>
            <div className="imageBox concertElement">
                <img src={apiLink + "/Images/" + props.concert.imageId} alt={props.concert.performer} className="concertImage" />
            </div>
            <div className="concertConcertType concertElement">
                { (props.concert.concertType === 0 ? "Классический" : props.concert.concertType === 1 ? "Открытое небо" : "Вечеринка").slice(0, widthEm / 7)}
            </div>
            <div className="concertCost concertElement">{props.concert.cost}$</div>
            <div className="concertLeftCount concertElement">{props.concert.leftCount}</div>
            <div className="concertPerformer concertElement">
                {props.concert.performer.length * 3 > widthEm ? props.concert.performer.slice(0, widthEm / 4) + "..." : props.concert.performer}
            </div>
            <div className="concertConcertDate concertElement">{new Date(props.concert.concertDate).toISOString().slice(0, 19).replace(/-/g, "/").replace("T", " ")}</div>
        </Link>
    );
}
export default ConcertSearchPageItem;