import ActionItem from "./ActionItem";

function ActionList(props) {
    return (
        <div className="actionList">
            {props.actionList.map(element => {
                return <ActionItem key = {element.creationTime} creationTime = {element.creationTime} description = {element.description}/>
            })}
        </div>
    );
}

export default ActionList;