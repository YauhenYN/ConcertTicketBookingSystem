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
    return <div className="ConcertsBox">
        <div className="MaincenterBox">
            <div className='NewestConcertBox'>
                <div className="mainImageBox">
                    <img src={apiLink + "/Images/" + props.concerts[indexes[0]()].imageId} alt={"image1"} className="mainConcertImage" />
                </div>
                <div className="mainConcertBottonBox">
                    <p className="MainConcertPerformer">{props.concerts[indexes[0]()].performer}</p>
                    <Link to={to(props.concerts[indexes[0]()].concertId)} className="concertButtonLink">
                        <Button text="Перейти" />
                    </Link>
                </div>
            </div>
            <div className='NewestConcertBox'>
                <div className="mainImageBox">
                    <img src={apiLink + "/Images/" + props.concerts[indexes[1]()].imageId} alt={"image2"} className="mainConcertImage" />
                </div>
                <div className="mainConcertBottonBox">
                    <p className="MainConcertPerformer">{props.concerts[indexes[1]()].performer}</p>
                    <Link to={to(props.concerts[indexes[1]()].concertId)} className="concertButtonLink">
                        <Button text="Перейти" />
                    </Link>
                </div>
            </div>
            <div className='NewestConcertBox'>
                <div className="mainImageBox">
                    <img src={apiLink + "/Images/" + props.concerts[indexes[2]()].imageId} alt={"image3"} className="mainConcertImage" />
                </div>
                <div className="mainConcertBottonBox">
                    <p className="MainConcertPerformer">{props.concerts[indexes[2]()].performer}</p>
                    <Link to={to(props.concerts[indexes[2]()].concertId)} className="concertButtonLink">
                        <Button text="Перейти" />
                    </Link>
                </div>
            </div>
        </div>
        <div className="elementButtonsBox">
            <PreviousElementButton onClick={PreviousElementButtonOnClick(index, setIndex, props.concerts.length - 1)} />
            <NextElementButton onClick={NextElementButtonOnClick(index, setIndex)} />
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