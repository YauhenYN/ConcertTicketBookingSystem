function ActionItem(props) {
    return (
        <div className="actionItem">
            <div className="actionCreationTime">
                {props.creationTime}
            </div>
            <div className="actionDescription">
                {props.description}
            </div>
        </div>
    );
}

export default ActionItem;