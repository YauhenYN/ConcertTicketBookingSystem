import { useState } from 'react';
import './ActionList.css';
import "./PromoCodeList.css";
import "./ConcertsList.css";
import ConcertItem from "./ConcertItem";
import NextPageButton from '../../../CommonElements/NextPageButton';
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";

function ConcertsList(props) {
    const [concerts, setConcerts] = useState(props.firstConcerts);
    const [pageNumber, setPageNumber] = useState(0);
    return (
        <div className="outOfListTable">
            <div className="ListTable">
                <div className="ListHeader">
                    <div className="isActiveActionList">Актив</div>
                    <div className="concertTypeActionList">Тип</div>
                    <div className="costActionList">Стоим.</div>
                    <div className="leftCountActionList">Остаток</div>
                    <div className="performerActionList">Исполн.</div>
                    <div className="concertDateActionList">Дата</div>
                    <div className="activateButtonActionList"></div>
                </div>
                <div className="ListItemFirst" />
                {concerts.map(concert => {
                    return <ConcertItem key={concert.concertId} concert={concert} />
                })}
                {pageNumber + 1 < props.pagesCount && <NextPageButton onClick={AddNextPage(pageNumber, setPageNumber, concerts, setConcerts)} />}
            </div>
        </div>
    );
}
function AddNextPage(pageNumber, setPageNumber, concerts, setConcerts) {
    return function action() {
        store.dispatch(actionCreators.GetManyLightConcertsActionCreator(pageNumber + 1, 30, null, null, null, null, store.getState().user.userId, null, null, null, null, null, null, null, 1)).then((result) => {
            setConcerts([...concerts, ...result.data.concerts]);
            setPageNumber(pageNumber + 1);
        }).catch(() => { });
    }
}

export default ConcertsList;