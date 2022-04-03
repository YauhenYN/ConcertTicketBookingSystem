import { apiLink } from "../../../configuration";
import './ConcertSearchPageItem.css';

const widthEEm = () => {
    return window.innerWidth / parseFloat(
        getComputedStyle(
            document.querySelector('body')
        )['font-size'])
}

function ConcertSearchPageItem(props) {
    return (
        <div className="searchPageItem">
            <div className="imageBox">
                <img src={apiLink + "/Images/" + props.concert.imageId} alt={props.concert.performer} className="concertImage" />
            </div>
            <div className="concertCost">{props.concert.cost}</div>
            <div className="concertLeftCount">{props.concert.leftCount}</div>
            <div className="concertPerformer">{props.concert.performer}</div>
            <div className="concertConcertDate">{props.concert.concertDate}</div>
        </div>
    );
}
export default ConcertSearchPageItem;