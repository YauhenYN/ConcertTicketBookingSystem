
function UserBriefInfoItem(props) {
    return <div className="ListItem">
        <div className="UserListId">
            {props.user.userId}
        </div>
        <div className="UserListName">
            {props.user.name}
        </div>
        <div className="UserListIsAdmin">
            {props.user.isAdmin ? "Да" : "Нет"}
        </div>
        <div className="UserListEmail">
            {props.user.email}
        </div>
    </div >
}

export default UserBriefInfoItem;