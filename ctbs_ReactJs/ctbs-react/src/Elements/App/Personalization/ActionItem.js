import { toCommonDateFormat, toLocaleDate } from "../../../configuration";

function ActionItem(props) {

    return (
        <div className="actionItem">
            <div className="actionCreationTime">
                {toCommonDateFormat(toLocaleDate(toLocaleDate(new Date(props.creationTime))).toISOString())}
            </div>
            <div className="actionDescription">
                {props.description}
            </div>
        </div>
    );
}

export default ActionItem;