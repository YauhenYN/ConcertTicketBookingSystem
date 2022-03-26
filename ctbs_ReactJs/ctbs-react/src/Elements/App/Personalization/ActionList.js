import { useEffect } from "react";
import ActionItem from "./ActionItem";
import './ActionList.css';


function ActionList(props) {
    useEffect(() => {
        scrollToBottom("actionTable");
    }, [])
    return (
        <div id = "outOfActionTable">
        <div id = "actionTable">
            <div id = "actionHeader">
                <div id = "actionHeaderCreationTime" className="actionCreationTime">Дата и время</div>
                <div id = "actionHeaderDescription" className="actionDescription">Описание</div>
            </div>
            <div className="actionItem"/><div className="actionItem"/>
            {props.actionList.map(element => {
                return <ActionItem key={element.creationTime} creationTime={new Date(Date.parse(element.creationTime)).toLocaleString()} description={element.description} />
            })}
        </div>
        </div>
    );
}
const scrollToBottom = (id) => {
    const element = document.getElementById(id);
    element.scrollTop = element.scrollHeight;
}
export default ActionList;