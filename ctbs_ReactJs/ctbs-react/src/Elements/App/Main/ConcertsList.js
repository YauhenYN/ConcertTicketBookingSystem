import { useState } from "react";
import Button from "../../../CommonElements/Button";
import NextElementButton from "../../../CommonElements/NextElementButton";
import PreviousElementButton from "../../../CommonElements/PreviousElementButton";
import { Link } from "react-router-dom";
import { apiLink } from "../../../configuration";

const to = concertId => "/Concerts/" + concertId;

function ConcertsList(props) {
    const [index, setIndex] = useState(0);
    const indexes = [() => index - (Math.floor(index / props.concerts.length) * props.concerts.length),
    () => index + 1 - (Math.floor((index + 1) / props.concerts.length) * props.concerts.length),
    () => index + 2 - (Math.floor((index + 2) / props.concerts.length) * props.concerts.length)
    ]
    const [imageElements] = useState([...props.concerts.map((concert) => {
        return <div key={concert.concertId} className='NewestConcertBox'>
            <div className="mainImageBox">
                <img src={apiLink + "/Images/" + concert.imageId} alt={"image1"} className="mainConcertImage" />
            </div>
            <div className="mainConcertBottonBox">
                <p className="MainConcertPerformer">{concert.performer}</p>
                <Link to={to(concert.concertId)} className="concertButtonLink">
                    <Button text="Перейти" />
                </Link>
            </div>
        </div>
    })]);
    return <div className="ConcertsBox">
        <div className="elementButtonsBox">
            <PreviousElementButton onClick={PreviousElementButtonOnClick(index, setIndex, props.concerts.length - 1)} />
            <NextElementButton onClick={NextElementButtonOnClick(index, setIndex)} />
        </div>
        <div className="MaincenterBox">
            {imageElements[indexes[0]()]}
            {imageElements[indexes[1]()]}
            {imageElements[indexes[2]()]}
        </div>
    </div >;
}

function PreviousElementButtonOnClick(index, setIndex, lenght) {
    return function action() {
        index === 0 ? setIndex(lenght) : setIndex(index - 1)
    }
}
function NextElementButtonOnClick(index, setIndex) {
    return function action() {
        setIndex(index + 1)
    }
}
export default ConcertsList;