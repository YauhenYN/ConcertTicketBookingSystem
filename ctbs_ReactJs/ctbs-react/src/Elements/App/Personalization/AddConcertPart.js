import * as actionCreators from "../../../actionCreators";
import React, { useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";
import NumberInput from "../../../CommonElements/NumberInput";
import CheckBoxInput from "../../../CommonElements/CheckBoxInput";
import FileInput from "../../../CommonElements/FileInput";
import DateTimeInput from "../../../CommonElements/DateTimeInput";
import './AddConcertPart.css';
import RadioInput from "../../../CommonElements/RadioInput";
import GoogleMapReact from 'google-map-react';

const AnyReactComponent = ({ text }) => <div className="MapMarker">{text}</div>;

function AddConcertPart() {
    const [addConcertModalWindow, setAddConcertModalWindow] = useState(false);
    //All
    const [isActiveFlag, setIsActiveFlag] = useState(true);
    // images.imageType images.image, images.imageName
    const [images, setImages] = useState([]);
    const [cost, setCost] = useState(0.01);
    const [totalCount, setTotalCount] = useState(1);
    const [performer, setPerformer] = useState("");
    const [concertDate, setConcertDate] = useState("");
    const [latitude, setLatiture] = useState();
    const [longitude, setLongitude] = useState();
    const [concertType, setConcetType] = useState("0");
    //ClassicConcert
    const [voiceType, setVoiceType] = useState("");
    const [concertName, setConcertName] = useState("");
    const [compositor, setCompositor] = useState("");
    //OpenAirConcert
    const [route, setRoute] = useState("");
    const [headLiner, setHeadLiner] = useState("");
    //PartyConcert
    const [censure, setCensure] = useState(0);
    const addConcertByConcertType = [
        () => addClassicConcert(isActiveFlag, images[0] && images[0].imageType, images[0] && images[0].image, cost, totalCount, performer, concertDate, latitude,
            longitude, concertType, setAddConcertModalWindow, voiceType, concertName, compositor, images.slice(1)),
        () => addOpenAirConcert(isActiveFlag, images[0] && images[0].imageType, images[0] && images[0].image, cost, totalCount, performer, concertDate, latitude,
            longitude, concertType, setAddConcertModalWindow, route, headLiner, images.slice(1)),
        () => addPartyConcert(isActiveFlag, images[0] && images[0].imageType, images[0] && images[0].image, cost, totalCount, performer, concertDate, latitude,
            longitude, concertType, setAddConcertModalWindow, censure, images.slice(1))];
    return (
        <form className="centerBox boxColumn addConcertBox" onSubmit={addConcertByConcertType[concertType]()}>
            <div className="bigTextCenter">Добавление концерта</div>
            <div className="boxRow radioBox">
                <RadioInput text="Классический концерт" value={0} onChange={event => setConcetType(event.currentTarget.value)} checked={concertType === "0"} />
                <RadioInput text="Концерт под открытым небом" value={1} onChange={event => setConcetType(event.currentTarget.value)} checked={concertType === "1"} />
                <RadioInput text="Концерт-вечеринка" value={2} onChange={event => setConcetType(event.currentTarget.value)} checked={concertType === "2"} />
            </div>
            <div id="mapWithSmallParamsBox">
                <div id="mapBox">
                    <GoogleMapReact
                        bootstrapURLKeys={{}}
                        defaultCenter={{
                            lat: 0,
                            lng: 0
                        }}
                        defaultZoom={1}
                        onClick={
                            (ev) => {
                                setLatiture(ev.lat);
                                setLongitude(ev.lng);
                            }
                        }>
                        <AnyReactComponent lat={latitude} lng={longitude} text="Место концерта" />
                    </GoogleMapReact>
                </div>
                <div id="smallParamsBox">
                    <div className="boxRow">
                        <CheckBoxInput isChecked={isActiveFlag} text="Статус активности" onChange={event => setIsActiveFlag(event.target.checked)} />
                    </div>
                    <div className="boxRow">
                        <div className="boxRowIn">
                            <div className="boxRowLeftText">Стоимость ($)</div>
                            <NumberInput value={cost} onChange={event => setCost(event.target.value)} min={0.01} max={1000} step=".01" />
                        </div>
                    </div>
                    <div className="boxRow">
                        <div className="boxRowIn">
                            <div className="boxRowLeftText">Количество</div>
                            <NumberInput value={totalCount} onChange={event => setTotalCount(event.target.value)} min={1} max={10000} />
                        </div>
                    </div>
                    <div className="boxRow datetimeBox">
                        <div className="boxRowIn">
                            <div className="boxRowLeftText">Дата и время</div>
                            <DateTimeInput value={concertDate} onChange={event => setConcertDate(event.target.value)} min={new Date().toISOString()} max = {addDays(new Date(), 100).toISOString()}/>
                        </div>
                    </div>

                    {concertType === "0" && <div className="boxRow">
                        <div className="boxRowIn">
                            <div className="boxRowLeftText">Тип голоса</div>
                            <TextInput value={voiceType} onChange={event => setVoiceType(event.target.value)} minLength={1} maxLength={10} />
                        </div>
                    </div>}
                </div>
            </div>
            {concertType === "0" && <>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Название концерта</div>
                        <TextInput value={concertName} onChange={event => setConcertName(event.target.value)} minLength={3} maxLength={20} />
                    </div>
                </div>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Композитор</div>
                        <TextInput value={compositor} onChange={event => setCompositor(event.target.value)} minLength={3} maxLength={30} />
                    </div>
                </div>
            </>}
            {concertType === "1" && <>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Маршрут</div>
                        <TextInput value={route} onChange={event => setRoute(event.target.value)} minLength={1} maxLength={200} />
                    </div>
                </div>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Хедлайнер</div>
                        <TextInput value={headLiner} onChange={event => setHeadLiner(event.target.value)} minLength={3} maxLength={30} />
                    </div>
                </div>
            </>}
            {concertType === "2" && <>
                <div className="boxRow">
                    <div className="boxRowIn">
                        <div className="boxRowLeftText">Цензура (возраст)</div>
                        <NumberInput value={censure} onChange={event => setCensure(event.target.value)} min={3} max={21} />
                    </div>
                </div>
            </>}
            <div className="boxRow">
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Исполнитель</div>
                    <TextInput value={performer} onChange={event => setPerformer(event.target.value)} minLength={3} maxLength={50} />
                </div>
            </div>
            <div className="boxRow preLoaderLoad">
                <div className="imageLoaderBox">
                    {images[0] && images[0].imageName && <div className="imageNameText">{images[0].imageName.slice(0, 5)}... (Главная)</div>}
                    {images.slice(1).map(image => <div key={image.imageName} className="imageNameText">{image.imageName.slice(0, 5)}...</div>)}
                    {images.length < 5 && <FileInput text="Загрузить картинки" multiple="multiple" accept="image/png, image/jpeg" onChange={loadImage(images, setImages)} />}
                </div>
            </div>
            <SubmitButton text="Создать" />
            {addConcertModalWindow && <SimpleModalWindow text="Концерт создан" buttonText="Ok" onClick={closeAddConcertModalWindow(setAddConcertModalWindow)} />}
        </form>
    );
}
function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }
function addClassicConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, setAddConcertModalWindow, voiceType, concertName, compositor, leftImages) {
    return function action(event) {
        event.preventDefault();
        if (image != null) {
            store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
                performer, concertDate, latitude, longitude, parseInt(concertType), {
                voiceType: voiceType,
                concertName: concertName,
                compositor: compositor
            }, null, null)).then((result) => {
                leftImages.forEach(image => {
                    store.dispatch(addImage(result.data.concertId, image.imageType, image.image));
                })
                setAddConcertModalWindow(true);
            });
        }
    }
}



function addOpenAirConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, setAddConcertModalWindow, route, headLiner, leftImages) {
    return function action(event) {
        event.preventDefault();
        if (image != null) {
            store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
                performer, concertDate, latitude, longitude, parseInt(concertType), null, {
                route: route,
                headLiner: headLiner
            }, null)).then((result) => {
                leftImages.forEach(image => {
                    store.dispatch(addImage(result.data.concertId, image.imageType, image.image));
                })
                setAddConcertModalWindow(true);
            });
        }
    }
}

function addPartyConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, setAddConcertModalWindow, censure, leftImages) {
    return function action(event) {
        event.preventDefault();
        if (image != null) {
            store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
                performer, concertDate, latitude, longitude, parseInt(concertType), null, null, {
                censure: censure
            })).then((result) => {
                leftImages.forEach(image => {
                    store.dispatch(addImage(result.data.concertId, image.imageType, image.image));
                })
                setAddConcertModalWindow(true);
            });
        }
    }
}

function addImage(concertId, imageType, image) {
    return function action(event) {
        store.dispatch(actionCreators.AddImageActionCreator(imageType, image, concertId));
    }
}

function closeAddConcertModalWindow(addConcertModalWindow) {
    return function action() {
        addConcertModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function loadImage(images, setImages) {
    return async function action(e) {
        function getImage(file) {
            const reader = new FileReader()
            return new Promise(resolve => {
                reader.onload = ev => {
                    resolve({
                        imageType: file.type,
                        image: ev.target.result.split(",")[1],
                        imageName: file.name
                    });
                }
                reader.readAsDataURL(file);
            })
        }

        e.preventDefault();
        const promises = [];
        Array.prototype.forEach.call(e.target.files, async file => {
            if (!images.some(image => file.name === image.imageName)) {
                promises.push(getImage(file));
            }
        });
        Promise.all(promises).then((inImages) => {
                const max = 5 - images.length;
                setImages([...images, ...inImages.slice(0, max)]);
        });
    }
}

export default AddConcertPart;