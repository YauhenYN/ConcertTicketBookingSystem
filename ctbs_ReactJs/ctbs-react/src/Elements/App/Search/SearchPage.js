import { useState } from 'react';
import NextPageButton from '../../../CommonElements/NextPageButton';
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";
import { useEffect } from 'react';
import './SearchPage.css'
import RadioInput from '../../../CommonElements/RadioInput';
import TextInput from '../../../CommonElements/TextInput';
import NumberInput from '../../../CommonElements/NumberInput';
import Button from '../../../CommonElements/Button';
import { useLocation } from 'react-router-dom';
import GoogleMapReact from 'google-map-react';
import { Link } from 'react-router-dom';
import ConcertSearchPageItem from './ConcertSearchPageItem';
import Loading from '../../App/Loading';

const AnyReactComponent = ({ text }) => <div className="MapMarker">{text}</div>;

function SearchPage() {
    const [isLoading, setIsLoading] = useState(true);
    const { state } = useLocation();
    const [concerts, setConcerts] = useState([]);
    const [pageNumber, setPageNumber] = useState(0)
    const [pagesCount, setPagesNumber] = useState();
    const [byConcertType, setByConcertType] = useState("");
    const [fromPrice, setFromPrice] = useState(0.01);
    const [untilPrice, setUntilPrice] = useState(1000);
    useEffect(() => {
        state && state.performer && store.dispatch(actionCreators.UpdateSearchPerformerActionCreator(state.performer));
        async function dispatches() {
            setIsLoading(true);
            await store.dispatch(actionCreators.GetManyLightConcertsActionCreator(0, 30, byConcertType, store.getState().search.performer === "Поиск (Исполнитель)" ? null : store.getState().search.performer, untilPrice, fromPrice, null, true)).then((result) => {
                setConcerts(result.data.concerts);
                setPagesNumber(result.data.pagesCount);
                pageNumber === 0 && setPageNumber(pageNumber + 1);
                setIsLoading(false);
            });
        }
        dispatches();
    }, [state])
    return <div className='element-common'>
        <div className='concertTypeHeader'>
            <RadioInput text="Все типы концертов" value={""} onChange={event => setByConcertType(event.currentTarget.value)} checked={byConcertType === ""} />
            <RadioInput text="Классический концерт" value={0} onChange={event => setByConcertType(event.currentTarget.value)} checked={byConcertType === "0"} />
            <RadioInput text="Концерт под открытым небом" value={1} onChange={event => setByConcertType(event.currentTarget.value)} checked={byConcertType === "1"} />
            <RadioInput text="Концерт-вечеринка" value={2} onChange={event => setByConcertType(event.currentTarget.value)} checked={byConcertType === "2"} />
        </div>
        <div className='SearchHeader'>
            <div className='inputBox'>
                <div className="boxRowLeftText">Исполнитель</div>
                <TextInput value={store.getState().search.performer} onChange={event => store.dispatch(actionCreators.UpdateSearchPerformerActionCreator(event.target.value))} minLength={0} maxLength={50} />
            </div>
            <div className='inputBox numberBox'>
                <div className="boxRowLeftText">Стоимость (от $)</div>
                <NumberInput value={fromPrice} onChange={event => setFromPrice(event.target.value)} min={0.01} max={1000} step=".01" />
            </div>
            <div className='inputBox numberBox'>
                <div className='inputBox numberBox'></div>
                <div className="boxRowLeftText">Стоимость (до $)</div>
                <NumberInput value={untilPrice} onChange={event => setUntilPrice(event.target.value)} min={0.01} max={1000} step=".01" />
            </div>
        </div>
        <div id='searchButton'><Link to="/search" state={{ performer: store.getState().search.performer }}><Button text="Найти" /></Link></div>
        {!isLoading ? concerts.length > 0 ? <div id="resultBox">
            <div id="mapSearch">
                <GoogleMapReact
                    bootstrapURLKeys={{}}
                    defaultCenter={{
                        lat: 0,
                        lng: 0
                    }}
                    defaultZoom={1}>
                    {
                        concerts.map(concert => {
                            return <AnyReactComponent lat={concert.latitude} lng={concert.longitude} text={concert.performer} key={concert.concertId} />
                        })
                    }
                </GoogleMapReact>
            </div>
            <div id="concertsListHeader" className="searchPageItem">
                <div className="imageBox concertElement">
                    <div className="concertImage" />
                </div>
                <div className="concertConcertType concertElement">Тип</div>
                <div className="concertCost concertElement">Стоимость</div>
                <div className="concertLeftCount concertElement">Осталось</div>
                <div className="concertPerformer concertElement">
                    Исполнитель
                </div>
                <div className="concertConcertDate concertElement">Дата концерта</div>
            </div>
            {concerts.map(concert => {
                return <ConcertSearchPageItem key={concert.concertId} concert={concert} />
            })}
            {pageNumber + 1 < pagesCount && <NextPageButton onClick={AddNextPage(pageNumber, setPageNumber, concerts, setConcerts, byConcertType, store.getState().search.performer === "Поиск (Исполнитель)" ? null : store.getState().search.performer, untilPrice, fromPrice, true)} />}
        </div> :
            <div className='notFoundCenter'>Not Found</div> :
            <Loading/>}
    </div>
}
function AddNextPage(pageNumber, setPageNumber, concerts, setConcerts, byConcertType, byPerformer, untilPrice, fromPrice, byActivity) { //nextPage, neededCount, byConcertType, byPerformer, untilPrice, fromPrice, byUserId
    return function action() {
        store.dispatch(actionCreators.GetManyLightConcertsActionCreator(pageNumber, 30, byConcertType, byPerformer === "Поиск (Исполнитель)" ? null : byPerformer, untilPrice, fromPrice, null, byActivity)).then((result) => {
            setConcerts([...concerts, ...result.data.concerts])
            setPageNumber(pageNumber + 1);
        }).catch(() => {
            setConcerts([]);
            setPageNumber(pageNumber);
        });
    }
}

export default SearchPage;