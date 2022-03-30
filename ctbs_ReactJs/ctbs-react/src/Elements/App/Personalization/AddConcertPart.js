import * as actionCreators from "../../../actionCreators";
import React, { useState } from 'react';
import store from "../../../store";
import './Personalization.css';
import TextInput from "../../../CommonElements/TextInput";
import SimpleModalWindow from "../../../CommonElements/SimpleModalWindow";
import SubmitButton from "../../../CommonElements/SubmitButton";
import NumberInput from "../../../CommonElements/NumberInput";
import CheckBoxInput from "../../../CommonElements/CheckBoxInput";
import Button from "../../../CommonElements/Button";
import FileInput from "../../../CommonElements/FileInput";
import './AddConcertPart.css';

function AddConcertPart() {
    const [addConcertModalWindow, setAddConcertModalWindow] = useState(false);
    //All
    const [isActiveFlag, setIsActiveFlag] = useState(true);
    const [imageType, setImageType] = useState();
    const [image, setImage] = useState();
    const [imageName, setImageName] = useState();
    const [cost, setCost] = useState(0.01);
    const [totalCount, setTotalCount] = useState();
    const [performer, setPerformer] = useState();
    const [concertDate, setConcertDate] = useState();
    const [latitude, setLatiture] = useState();
    const [longitude, setLongitude] = useState();
    const [concertType, setConcetType] = useState(0);
    //ClassicConcert
    const [voiceType, setVoiceType] = useState();
    const [concertName, setConcertName] = useState();
    const [compositor, setCompositor] = useState();
    //OpenAirConcert
    const [route, setRoute] = useState();
    const [headLiner, setHeadLiner] = useState();
    //PartyConcert
    const [censure, setCensure] = useState(0);
    const addConcertByConcertType = [
        () => addClassicConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
            longitude, concertType, setAddConcertModalWindow, voiceType),
        () => addOpenAirConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
            longitude, concertType, setAddConcertModalWindow, route, headLiner),
        () => addPartyConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
            longitude, concertType, setAddConcertModalWindow, censure)];
    return (
        <form className="centerBox boxColumn" onSubmit={addConcertByConcertType[concertType]}>
            <div className="bigTextCenter">Добавление концерта</div>
            <div className="boxRow">
                <CheckBoxInput isChecked={isActiveFlag} text="Активный" onChange={event => setIsActiveFlag(event.target.checked)} />
            </div>
            <div className="boxRow">
                <div className="imageLoaderBox">
                    <div>{imageName}</div>
                    <FileInput text="Загрузить картинку" onChange={loadImage(setImage, setImageType, setImageName)} />
                </div>
            </div>
            <div className="boxRow">
                <div className="boxRowIn">
                    <div className="boxRowLeftText">Стоимость ($)</div>
                    <NumberInput value={cost} onChange={event => setCost(event.target.value)} min={0.01} max={1000} step=".01" />
                </div>
            </div>
            <SubmitButton text="Создать" />
            {addConcertModalWindow && <SimpleModalWindow text="Концерт создан" buttonText="Ok" onClick={closeAddConcertModalWindow(setAddConcertModalWindow)} />}
        </form>
    );
}

function addClassicConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, setAddConcertModalWindow, voiceType, concertName, compositor) {
    return function action(event) {
        event.preventDefault();
        store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
            performer, concertDate, latitude, longitude, concertType, {
            voiceType: voiceType,
            concertName: concertName,
            compositor: compositor
        }, null, null)).then(() => {
            setAddConcertModalWindow(true);
        });
    }
}

function addOpenAirConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, setAddConcertModalWindow, route, headLiner) {
    return function action(event) {
        event.preventDefault();
        store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
            performer, concertDate, latitude, longitude, concertType, null, {
            route: route,
            headLiner: headLiner
        }, null)).then(() => {
            setAddConcertModalWindow(true);
        });
    }
}

function addPartyConcert(isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude,
    longitude, concertType, setAddConcertModalWindow, censure) {
    return function action(event) {
        event.preventDefault();
        store.dispatch(actionCreators.AddConcertActionCreator(isActiveFlag, imageType, image, cost, totalCount,
            performer, concertType, latitude, longitude, concertDate, null, null, {
            censure: censure
        })).then(() => {
            setAddConcertModalWindow(true);
        });
    }
}

function closeAddConcertModalWindow(addConcertModalWindow) {
    return function action() {
        addConcertModalWindow(false);
        document.querySelector("body").style.overflow = "auto";
    }
}
function loadImage(setImage, setImageType, setImageName) {
    return function action(e) {
        e.preventDefault();
        let reader = new FileReader();
        let file = e.target.files[0];
        reader.onloadend = () => {
            setImageType(file.type);
            setImage(reader.result);
            setImageName(file.name);
        }

        reader.readAsDataURL(file)
    }
}

export default AddConcertPart;