import ActionList from "./ActionList";
import * as actionCreators from "../../../actionCreators";
import React, { useEffect, useState } from 'react';
import Loading from "../Loading";
import store from "../../../store";
import './Personalization.css';
import PersonalizationPart from "./PersonalizationPart";
import AdministrationPart from "./AdministrationPart";
import CreatePromoCodePart from "./CreatePromoCodePart";
import PromoCodeList from "./PromoCodeList";
import ConcertsList from "./ConcertsList";
import AddConcertPart from "./AddConcertPart";
import TicketsList from "./TicketsList";
import TicketAdministrationPart from "./TicketAdministrationPart";

function Personalization() {
    const [actions, setActions] = useState();
    const [activePromoCodes, setActivePromoCodes] = useState([]);
    const [inactivePromoCodes, setInactivePromoCodes] = useState([]);
    const [isLoading, setisLoading] = useState(true);
    const [userPromoCode, setuserPromoCode] = useState({});
    const [firstConcerts, setFirstConcerts] = useState([]);
    const [pagesCount, setPagesCount] = useState();
    const [firstTickets, setFirstTickets] = useState([]);
    const [ticketPagesCount, setTicketPagesCount] = useState();
    useEffect(() => {
        async function dispatches() {
            await store.dispatch(actionCreators.GetActionsActionCreator()).then((result) => {
                setActions(result.data);
            });
            await store.dispatch(actionCreators.GetManyTicketsActionCreator(0, store.getState().user.userId, null, 15)).then(result => {
                setFirstTickets([...result.data.tickets]);
                setTicketPagesCount(result.data.pageCount);
            })
            if (store.getState().user.isAdmin) {
                await store.dispatch(actionCreators.GetManyPromocodesActionCreator(true, 1000)).then((result) => {
                    setActivePromoCodes([...result.data]);
                }).catch(() => { });
                await store.dispatch(actionCreators.GetManyPromocodesActionCreator(false, 1000)).then((result) => {
                    setInactivePromoCodes([...result.data]);
                }).catch(() => { });
                await store.dispatch(actionCreators.GetManyLightConcertsActionCreator(0, 30, null, null, 10000, 0.01, store.getState().user.userId)).then((result) => {
                    setFirstConcerts([...result.data.concerts]);
                    setPagesCount(result.data.pagesCount);
                }).catch(() => { });
            };
            if (store.getState().user.promoCodeId !== null) {
                await store.dispatch(actionCreators.GetPromoCodeByIdThunkActionCreator(store.getState().user.promoCodeId))
                    .then((result) => {
                        if (result.data.isActiveFlag) setuserPromoCode({ ...result.data });
                    });
            }
            else {
                setuserPromoCode({});
            }
        };
        dispatches().then(() => {
            setisLoading(false);
        }).catch(() => {
            setisLoading(false);
        });
    }, []);
    return (<>
        {isLoading === true ? (<Loading />) : (<div className="element-common">
            <div className="textHeader">Персонализация</div>
            <PersonalizationPart userPromoCode={userPromoCode} />
            <div className="textHeader">Купленные билеты</div>
            <TicketsList firstTickets={firstTickets} pagesCount={ticketPagesCount} />
            {store.getState().user.isAdmin && <>
                <div className="textHeader">Администрирование</div>
                <AdministrationPart />
                <CreatePromoCodePart />
                <TicketAdministrationPart />
                {(activePromoCodes.length > 0 || inactivePromoCodes.length > 0) && <>
                    <div className="textHeader">Список всех промокодов</div>
                    <PromoCodeList promoCodeList={[...activePromoCodes, ...inactivePromoCodes]} /></>}
                {firstConcerts.length > 0 && <>
                    <div className="textHeader">Список моих концертов</div>
                    <ConcertsList firstConcerts={firstConcerts} pagesCount={pagesCount} />
                </>}
                <AddConcertPart />
            </>}
            <div className="textHeader">Последние действия</div>
            <ActionList actionList={actions} />
        </div>)
        }</>
    );
}

export default Personalization;