function ActionItem(props) {
    return (
        <div className="actionItem">
            <div className="actionCreationTime">
                {new Date(props.creationTime).toISOString().slice(0, 19).replace(/-/g, "/").replace("T", " ")}
            </div>
            <div className="actionDescription">
                {props.description}
            </div>
        </div>
    );
}

export default ActionItem;