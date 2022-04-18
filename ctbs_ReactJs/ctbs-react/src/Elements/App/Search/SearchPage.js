import { useState } from 'react';
import NextPageButton from '../../../CommonElements/NextPageButton';
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";
import { useEffect } from 'react';
import './SearchPage.css'
import RadioInput from '../../../CommonElements/RadioInput';
import TextInput from '../../../CommonElements/TextInput';
import NumberInput from '../../../CommonElements/NumberInput';
import DateInput from '../../../CommonElements/DateInput';
import SubmitButton from '../../../CommonElements/SubmitButton';
import { useLocation } from 'react-router-dom';
import GoogleMapReact from 'google-map-react';
import ConcertSearchPageItem from './ConcertSearchPageItem';
import Loading from '../../App/Loading';
import groupByToMap from 'array.prototype.groupbytomap';

const AnyReactComponent = ({ text }) => <div className="MapMarker">{text}</div>;

function SearchPage() {
    const [isLoading, setIsLoading] = useState(true);
    const { state } = useLocation();
    const [concerts, setConcerts] = useState([]);
    const [pageNumber, setPageNumber] = useState(0)
    const [pagesCount, setPagesNumber] = useState();
    const [byConcertType, setByConcertType] = useState("");
    const [byConcertName, setByConcertName] = useState("");
    const [byVoiceType, setByVoiceType] = useState("");
    const [byHeadLiner, setByHeadLiner] = useState("");
    const [byCompositor, setByCompositor] = useState("");
    const [fromPrice, setFromPrice] = useState(0.01);
    const [untilPrice, setUntilPrice] = useState(1000);
    const [dateFrom, setDateFrom] = useState(new Date().toISOString().split('T')[0]);
    const [dateUntil, setDateUntil] = useState(addDays(new Date(), 100).toISOString().split('T')[0]);

    useEffect(() => {
        state && state.performer && store.dispatch(actionCreators.UpdateSearchPerformerActionCreator(state.performer));
        async function dispatches() {
            await FirstPage(setIsLoading, setPagesNumber, pageNumber, setPageNumber, setConcerts,
                byConcertType, store.getState().search.performer,
                untilPrice, fromPrice, true, byConcertName, byVoiceType, byHeadLiner, new Date(dateFrom), dateUntil, byCompositor)();
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
        <form className='SearchHeader' onSubmit={FirstPage(setIsLoading, setPagesNumber, pageNumber, setPageNumber, setConcerts,
            byConcertType, store.getState().search.performer,
            untilPrice, fromPrice, true, byConcertName, byVoiceType, byHeadLiner, new Date(dateFrom), new Date(dateUntil), byCompositor)}>
            <div className='toRightSideBox'>
                <div className='inputBox'>
                    <div className="boxRowLeftText">Исполнитель</div>
                    <TextInput value={store.getState().search.performer} required={false} onChange={event => store.dispatch(actionCreators.UpdateSearchPerformerActionCreator(event.target.value))} maxLength={50} />
                </div>
                {byConcertType === "0" && <>
                    <div className='inputBox'>
                        <div className="boxRowLeftText">Название</div>
                        <TextInput value={byConcertName} required={false} onChange={event => setByConcertName(event.target.value)} maxLength={20} />
                    </div>
                    <div className='inputBox'>
                        <div className="boxRowLeftText">Композитор</div>
                        <TextInput value={byCompositor} required={false} onChange={event => setByCompositor(event.target.value)} maxLength={30} />
                    </div>
                    <div className='inputBox'>
                        <div className="boxRowLeftText">Тип голоса</div>
                        <TextInput value={byVoiceType} required={false} onChange={event => setByVoiceType(event.target.value)} maxLength={10} />
                    </div></>}
                {byConcertType === "1" && <div className='inputBox'>
                    <div className="boxRowLeftText">Хэдлайнер</div>
                    <TextInput value={byHeadLiner} required={false} onChange={event => setByHeadLiner(event.target.value)} maxLength={30} />
                </div>}
            </div>
            <div className='lineBox'>
                <div className='inputBox leftLineBox numberBox'>
                    <div className="boxRowLeftText">Стоимость (от $)</div>
                    <NumberInput value={fromPrice} onChange={event => setFromPrice(event.target.value)} min={0.01} max={1000} step=".01" />
                </div>
                <div className='inputBox numberBox'>
                    <div className='inputBox numberBox'></div>
                    <div className="boxRowLeftText">Стоимость (до $)</div>
                    <NumberInput value={untilPrice} onChange={event => setUntilPrice(event.target.value)} min={0.01} max={1000} step=".01" />
                </div>
            </div>
            <div className='lineBox'>
                <div className='inputBox leftLineBox'>
                    <div className="boxRowLeftText">Дата (от)</div>
                    <DateInput value={dateFrom} onChange={event => setDateFrom(event.target.value)} min={new Date().toISOString().split('T')[0]} max={addDays(new Date(), 100).toISOString().split('T')[0]} />
                </div>
                <div className='inputBox'>
                    <div className="boxRowLeftText">Дата (до)</div>
                    <DateInput value={dateUntil} onChange={event => setDateUntil(event.target.value)} min={new Date().toISOString().split('T')[0]} max={addDays(new Date(), 100).toISOString().split('T')[0]} />
                </div>
            </div>
            <div id='searchButton'><SubmitButton text="Найти" /></div>
        </form>
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
                        toMapComponents(concerts)
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
            {pageNumber < pagesCount && <NextPageButton onClick={AddNextPage(pageNumber, setPageNumber,
                concerts, setConcerts, byConcertType, store.getState().search.performer, untilPrice,
                fromPrice, true, byConcertName, byVoiceType, byHeadLiner, new Date(dateFrom), new Date(dateUntil), byCompositor)} />}
        </div> :
            <div className='notFoundCenter'>Not Found</div> :
            <Loading />}
    </div>
}
function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}
function FirstPage(setIsLoading, setPagesNumber, pageNumber, setPageNumber, setConcerts,
    byConcertType, byPerformer, untilPrice, fromPrice, byActivity,
    byConcertName, byVoiceType, byHeadLiner, dateFrom, dateUntil, byCompositor) {
    return async function action(event) {
        event && event.preventDefault();
        setIsLoading(true);
        return await store.dispatch(actionCreators.GetManyLightConcertsActionCreator(0, 30, byConcertType,
            byPerformer === "" ? null : byPerformer,
            untilPrice,
            fromPrice,
            null,
            byActivity,
            byConcertName === "" ? null : byConcertName,
            byVoiceType === "" ? null : byVoiceType,
            byHeadLiner === "" ? null : byHeadLiner,
            dateFrom,
            dateUntil,
            byCompositor === "" ? null : byCompositor
        )).then((result) => {
            setConcerts(result.data.concerts);
            setPagesNumber(result.data.pagesCount);
            pageNumber === 0 && setPageNumber(pageNumber + 1);
            setIsLoading(false);
        }).catch(() => {
            setConcerts([]);
            setIsLoading(false);
        });
    }
}

function AddNextPage(pageNumber, setPageNumber, concerts, setConcerts,
    byConcertType, byPerformer, untilPrice, fromPrice, byActivity,
    byConcertName, byVoiceType, byHeadLiner, dateFrom, dateUntil, byCompositor) {
    return function action() {
        store.dispatch(actionCreators.GetManyLightConcertsActionCreator(pageNumber, 30, byConcertType,
            byPerformer === "" ? null : byPerformer, untilPrice, fromPrice, null, byActivity,
            byConcertName === "" ? null : byConcertName,
            byVoiceType === "" ? null : byVoiceType,
            byHeadLiner === "" ? null : byHeadLiner,
            dateFrom,
            dateUntil,
            byCompositor === "" ? null : byCompositor
        )).then((result) => {
            setConcerts([...concerts, ...result.data.concerts])
            setPageNumber(pageNumber + 1);
        }).catch(() => {
            setConcerts([]);
            setPageNumber(pageNumber);
        });
    }
}

function GroupConcerts(concerts) {
    return groupByToMap(concerts, el => el.latitude + el.longitude);
}
function toMapComponents(concerts){
    let components = [];
    let index = 0;
    GroupConcerts(concerts).forEach((group) => {
        let text = "";
        Array.prototype.forEach.call(group, concert => { return text = text + " " + concert.performer });
        components[index] = <AnyReactComponent lat={group[0].latitude} lng={group[0].longitude} text={text} key={group[0].concertId} />
        index++;
    })
    return components;
}

export default SearchPage;