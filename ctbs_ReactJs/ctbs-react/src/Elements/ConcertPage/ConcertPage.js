import { useEffect } from "react";
import { useState } from "react";
import { useParams } from "react-router-dom";
import store from "../../store";
import * as actionCreators from "../../actionCreators";
import Loading from "../App/Loading";
import { apiLink } from "../../configuration";
import './ConcertPage.css';
import GoogleMapReact from 'google-map-react';
import Button from '../../CommonElements/Button';
import NumberInput from '../../CommonElements/NumberInput';

const AnyReactComponent = ({ text }) => <div className="MapMarker">{text}</div>;

function ConcertPage() {
    const { concertId } = useParams();
    const [isLoading, setIsLoading] = useState(true);
    const [concert, setConcert] = useState();
    const [pickedImage, setPickedImage] = useState(0);
    const [imageElements, setImageElements] = useState();
    const [count, setCount] = useState(1);
    const [promoCode, setPromoCode] = useState();
    useEffect(() => {
        async function dispatches() {
            await store.dispatch(actionCreators.GetFullConcertActionCreator(concertId)).then((result) => {
                setConcert(result.data);
                setImageElements([<img key={0} src={apiLink + "/Images/" + result.data.imageId} alt={result.data.performer} className="smallConcertImage" />,
                ...result.data.imageIds.map((id, index) =>
                    <img key={index + 1} src={apiLink + "/Images/" + id} alt={result.data.performer} className="smallConcertImage" />)]);
            });
            if(store.getState().user.promoCodeId) await store.dispatch(actionCreators.GetPromoCodeByIdThunkActionCreator(store.getState().user.promoCodeId)).then((result) => {
                setPromoCode(result.data);
            });
        }
        dispatches().then(() => {
            setIsLoading(false);
        })
    }, [concertId])
    return <>{!isLoading ? <div className="element-common">
        <div className="imagesBox">
            <div className="imageLeftSideConcert">
                <div className="topImageConcert">
                    {imageElements[pickedImage]}
                </div>
                <div className="leftImagesBox">
                    {imageElements.map(image => <div key={image.key} onClick={() => setPickedImage(image.key)} className="smallImageBox">{image}</div>)}
                </div>
            </div>
            <div className="rightSideConcertMenu">
                <div className="concertIsActive regularConcertText"><div className="MainPartConcertText">Статус: </div>{concert.isActiveFlag ? "Активен" : "Не активен"}</div>
                <div className="regularConcertText"><div className="MainPartConcertText">Тип: </div>{concert.concertType === 0 ? "Классический" : concert.concertType === 1 ? "Открытое небо" : "Вечеринка"}</div>
                <div className="regularConcertText"><div className="MainPartConcertText">Исполнитель: </div>{concert.performer}</div>
                {concert.concertType === 0 && <>
                    <div className="regularConcertText"><div className="MainPartConcertText">Название концерта: </div>{concert.classicConcertInfo.concertName}</div>
                    <div className="regularConcertText"><div className="MainPartConcertText">Композитор: </div>{concert.classicConcertInfo.compositor}</div>
                </>}
                {concert.concertType === 1 && <>
                    <div className="regularConcertText"><div className="MainPartConcertText">Хэдлайнер: </div>{concert.openAirConcertInfo.headLiner}</div>
                </>}
                <div className="regularConcertText"><div className="MainPartConcertText">Дата проведения: </div>{concert.concertDate}</div>
                {concert.isActiveFlag && <>
                    <div className="regularConcertText"><div className="MainPartConcertText">Стоимость: </div>{concert.cost}$</div>
                    <div className="regularConcertText"><div className="MainPartConcertText">Количество мест: </div>{concert.totalCount}</div>
                    <div className="regularConcertText"><div className="MainPartConcertText">Осталось билетов: </div>{concert.leftTicketsCount}</div>
                </>
                }
                <div className="regularConcertText"><div className="MainPartConcertText">Дата создания: </div>{concert.creationTime}</div>
                {concert.concertType === 0 && <>
                    <div className="regularConcertText"><div className="MainPartConcertText">Тип голоса: </div>{concert.classicConcertInfo.voiceType}</div>
                </>}
                {concert.concertType === 1 && <>
                    <div className="regularConcertText"><div className="MainPartConcertText">Маршрут: </div>{concert.openAirConcertInfo.route}</div>
                </>}
                {concert.concertType === 2 && <>
                    <div className="regularConcertText"><div className="MainPartConcertText">Допустимый возраст: </div>{concert.partyConcertInfo.censure}</div>
                </>}
                {concert.isActiveFlag && concert.leftTicketsCount > 0 && <>
                    <div className="outOfCountInput">
                        {promoCode && <div id = "discountConcertText" className="regularConcertText"><div className="MainPartConcertText">Скидка: </div>{promoCode.discount}$</div>}
                        <div className="regularConcertText"><div className="MainPartConcertText">Количество</div></div>
                        <NumberInput value={count} onChange={event => setCount(event.target.value)} min={1} max={5} />
                    </div>
                    <div className="buyTicketApi"><Button text="Купить" onClick={buyTicket(count, concert.concertId)} /></div>
                </>
                }
            </div>
        </div>
        <div className="mapConcertPage">
            <GoogleMapReact
                bootstrapURLKeys={{}}
                defaultCenter={{
                    lat: concert.latitude,
                    lng: concert.longitude
                }}
                defaultZoom={11}>
                {
                    <AnyReactComponent lat={concert.latitude} lng={concert.longitude} text={concert.performer} key={concert.concertId} />
                }
            </GoogleMapReact>
        </div>
    </div> :
        <Loading />}</>
}

function buyTicket(count, concertId) {
    return function action() {
        store.dispatch(actionCreators.BuyTicketActionCreator(count, concertId));
    }
}

export default ConcertPage;