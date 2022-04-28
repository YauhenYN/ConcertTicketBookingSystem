import "./TicketsPersonalizationList.css";
import { useEffect, useState } from "react";
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";
import NextPageButton from "../../../CommonElements/NextPageButton";
import UserBriefInfoItem from "./UserBriefInfoItem";
import './UserBriefInfoList.css';
import TextInput from "../../../CommonElements/TextInput";

function UsersBriefInfoListSearchInputEvent(byUserName, setUsersBriefInfo, setPagesCount, setNextPage) {
    return function Action(event) {
        if (event.key === "Enter") {
            event.preventDefault();
            store.dispatch(actionCreators.GetManyUsersBriefInfoActionCreator(0, null, byUserName, 15)).then(result => {
                setUsersBriefInfo([...result.data.users]);
                setPagesCount(result.data.pageCount);
                setNextPage(1);
            });
        }
    }
}

function UsersBriefInfoList(props) {
    const [usersBriefInfo, setUsersBriefInfo] = useState([]); //First Users
    const [byUserName, setByUserName] = useState("");
    const [nextPage, setNextPage] = useState(1);
    const [pagesCount, setPagesCount] = useState(); //PagesCount
    useEffect(() => {
        store.dispatch(actionCreators.GetManyUsersBriefInfoActionCreator(0, null, null, 15)).then(result => {
            setUsersBriefInfo([...result.data.users]);
            setPagesCount(result.data.pageCount);
        });
    }, []);
    return <>
        {usersBriefInfo.length > 0 && <>
            <div className="textHeader">Список пользователей</div>
            <div id="ticketsPersonalizationList" className="outOfListTable">
                <div className="ListTable">
                    <div className="ListHeader">
                        <div className="UserListId">Id</div>
                        <div className="UserListName">Имя
                            <div onKeyDown={UsersBriefInfoListSearchInputEvent(byUserName, setUsersBriefInfo, setPagesCount, setNextPage)}><TextInput value={byUserName} onChange={event => setByUserName(event.target.value)} maxLength={30} /></div>
                        </div>
                        <div className="UserListIsAdmin">Админ</div>
                        <div className="UserListEmail">Email</div>
                    </div>
                    <div className="ListItem" /><div className="ListItem" />
                    {usersBriefInfo.map(user => {
                        return <UserBriefInfoItem key={user.userId} user={user} />
                    })}
                    {nextPage < pagesCount && <NextPageButton onClick={loadUsersBriefInfo(null, byUserName.length > 0 ? byUserName : null, usersBriefInfo, setUsersBriefInfo, nextPage, setNextPage, setPagesCount)} />}
                </div>
            </div></>}
    </>
}

function loadUsersBriefInfo(byIsAdmin, byUserName, usersBriefInfo, setUsersBriefInfo, nextPage, setNextPage, setPagesCount) {
    return function action() {
        store.dispatch(actionCreators.GetManyUsersBriefInfoActionCreator(nextPage, byIsAdmin, byUserName, 15)).then(result => {
            setUsersBriefInfo([...usersBriefInfo, ...result.data.users]);
            setPagesCount(result.data.pageCount);
            setNextPage(nextPage + 1);
        })
    }
}

export default UsersBriefInfoList;