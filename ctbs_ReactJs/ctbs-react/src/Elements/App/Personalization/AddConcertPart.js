import * as actionCreators from "../../../actionCreators";
import React, { useEffect, useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";
import NumberInput from "../../../CommonElements/NumberInput";
import CheckBoxInput from "../../../CommonElements/CheckBoxInput";
import FileInput from "../../../CommonElements/FileInput";
import './AddConcertPart.css';
import RadioInput from "../../../CommonElements/RadioInput";
import GoogleMapReact from 'google-map-react';
import DateInput from "../../../CommonElements/DateInput";
import TimeInput from "../../../CommonElements/TimeInput";
import { toLocaleDate } from "../../../configuration";
import SelectInput from "../../../CommonElements/SelectInput";

const toZeroSecondsMilliseconds = (date) => {
    date.setSeconds(0, 0);
    date.setMilliseconds(0)
    return date;
}

const addMinute = (date) => {
    date.setMinutes(date.getMinutes() + 1);
    return date;
}
const AnyReactComponent = ({ text }) => <div className="MapMarker">{text}</div>;
function AddConcertPart() {
    const [ModalWindow, setModalWindow] = useState(false);
    const [ModalWindowText, setModalWindowText] = useState(false);
    //All
    const [isActiveFlag, setIsActiveFlag] = useState(true);
    // images.imageType images.image, images.imageName
    const [images, setImages] = useState([]);
    const [cost, setCost] = useState(0.01);
    const [totalCount, setTotalCount] = useState(1);
    const [performer, setPerformer] = useState("");
    const [concertDate, setConcertDate] = useState(toZeroSecondsMilliseconds(toLocaleDate(new Date())).toISOString().replace('Z', ""));
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
    const [isClickedSubmitButton, setIsClickedSubmitButton] = useState(0);
    const [minTime, setMinTime] = useState(new Date(concertDate) < new Date() ? toLocaleDate(addMinute(new Date())).toISOString().split('T')[1].substring(0, 5) : "");
    useEffect(() => {
        setMinTime(new Date(concertDate) < new Date() ? toZeroSecondsMilliseconds(toLocaleDate(addMinute(new Date()))).toISOString().split('T')[1].substring(0, 5) : "");
    }, [concertDate, isClickedSubmitButton]);
    useEffect(() => {
        if (latitude && longitude) {
            const element = document.getElementById("mapBox");
            element.style = "border: solid darkolivegreen";
        }
    }, [longitude, latitude]);
    useEffect(() => {
        if (images.length > 0) {
            const element = document.getElementsByClassName("imageLoaderBox")[0].getElementsByClassName("button")[0];
            if (element) element.style = "border: solid; color: darkolivegreen;";
        }
    }, [images])
    const addConcertByConcertType = [
        () => addClassicConcert(isActiveFlag, images[0] && images[0].imageType, images[0] && images[0].image, cost, totalCount, performer, !isNaN(new Date(concertDate)) && new Date(concertDate).toISOString(), latitude,
            longitude, concertType, voiceType, concertName, compositor, images.slice(1), setModalWindow, setModalWindowText),
        () => addOpenAirConcert(isActiveFlag, images[0] && images[0].imageType, images[0] && images[0].image, cost, totalCount, performer, !isNaN(new Date(concertDate)) && new Date(concertDate).toISOString(), latitude,
            longitude, concertType, route, headLiner, images.slice(1), setModalWindow, setModalWindowText),
        () => addPartyConcert(isActiveFlag, images[0] && images[0].imageType, images[0] && images[0].image, cost, totalCount, performer, !isNaN(new Date(concertDate)) && new Date(concertDate).toISOString(), latitude,
            longitude, concertType, censure, images.slice(1), setModalWindow, setModalWindowText)];
    return (
        <form className="centerBox boxColumn addConcertBox" onSubmit={addConcertByConcertType[concertType]()} encType="multipart/form-data">
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
                                ev.lat !== 0 && setLatiture(ev.lat);
                                ev.lng !== 0 && setLongitude(ev.lng);
                            }
                        }>
                        {latitude && longitude && <AnyReactComponent lat={latitude} lng={longitude} text="Место концерта" />}
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
                            <div className="boxRowLeftText">Дата</div>
                            <DateInput value={!isNaN(new Date(concertDate)) ? toLocaleDate(new Date(concertDate)).toISOString().split('T')[0] : ""} onChange={event => setConcertDate(event.target.value + 'T' + concertDate.split('T')[1])} min={new Date().toISOString().split('T')[0]} max={addDays(new Date(), 100).toISOString().split('T')[0]} />
                        </div>
                    </div>
                    <div className="boxRow datetimeBox">
                        <div className="boxRowIn">
                            <div className="boxRowLeftText">Время</div>
                            <TimeInput value={!isNaN(new Date(concertDate)) ? toZeroSecondsMilliseconds(toLocaleDate(new Date(concertDate))).toISOString().split('T')[1].replace('Z', "") : ""} onChange={event => setConcertDate(concertDate.split('T')[0] + 'T' + event.target.value)}
                                min={minTime} />
                        </div>
                    </div>
                    {concertType === "0" && <div className="boxRow">
                        <div className="boxRowIn">
                            <div className="boxRowLeftText">Тип голоса</div>
                            <SelectInput value={voiceType} onChange={event => setVoiceType(event.target.value)} values = {["Cопрано", "Меццо-сопрано", "Контральто", "Тенор", "Бас"]} />
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
                    {images.length < 5 && <FileInput text="Загрузить картинки" multiple="multiple" accept="image/png, image/jpeg" onChange={loadImage(images, setImages, setModalWindow, setModalWindowText)} />}
                </div>
            </div>
            <SubmitButton text="Создать" onClick={() => { setIsClickedSubmitButton(isClickedSubmitButton + 1) }} />
            {ModalWindow && <SimpleModalWindow text={ModalWindowText} buttonText="Ok" onClick={closeModalWindow(setModalWindow)} />}
        </form>
    );
}
function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}
function setMapModalWindows(setModalWindow, setModalWindowText){
    setModalWindowText("Пожалуйста, выберите место проведения концерта на карте");
    setModalWindow(true);
}
function addClassicConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, voiceType, concertName, compositor, leftImages, setModalWindow, setModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (!longitude || !latitude) {
            setMapModalWindows(setModalWindow, setModalWindowText);
        }
        else {
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
                    setModalWindowText("Концерт успешно добавлен");
                    setModalWindow(true);
                });
            }
        }
    }
}

function addOpenAirConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, route, headLiner, leftImages, setModalWindow, setModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (!longitude || !latitude) {
            setMapModalWindows(setModalWindow, setModalWindowText);
        }
        else {
            if (image != null) {
                store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
                    performer, concertDate, latitude, longitude, parseInt(concertType), null, {
                    route: route,
                    headLiner: headLiner
                }, null)).then((result) => {
                    leftImages.forEach(image => {
                        store.dispatch(addImage(result.data.concertId, image.imageType, image.image));
                    })
                    setModalWindowText("Концерт успешно добавлен");
                    setModalWindow(true);
                });
            }
        }
    }
}

function addPartyConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, censure, leftImages, setModalWindow, setModalWindowText) {
    return function action(event) {
        event.preventDefault();
        if (!longitude || !latitude) setMapModalWindows(setModalWindow, setModalWindowText);
        else {
            if (image != null) {
                store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
                    performer, concertDate, latitude, longitude, parseInt(concertType), null, null, {
                    censure: censure
                })).then((result) => {
                    leftImages.forEach(image => {
                        store.dispatch(addImage(result.data.concertId, image.imageType, image.image));
                    })
                    setModalWindowText("Концерт успешно добавлен");
                    setModalWindow(true);
                });
            }
        }
    }
}

function addImage(concertId, imageType, image) {
    return function action(event) {
        store.dispatch(actionCreators.AddImageActionCreator(imageType, image, concertId));
    }
}
function closeModalWindow(ModalWindow) {
    return function action() {
        ModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function loadImage(images, setImages, setModalWindow, setModalWindowText) {
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
            if (!images.some(image => file.name === image.imageName) && file.size < 200000) {
                promises.push(getImage(file));
            }
            if(file.size >= 200000){
                console.log("sdfgdgs");
                setModalWindowText("Изображение не должно весить более 190кб");
                setModalWindow(true);
            }
        });
        Promise.all(promises).then((inImages) => {
            const max = 5 - images.length;
            setImages([...images, ...inImages.slice(0, max)]);
        });
    }
}

export default AddConcertPart;