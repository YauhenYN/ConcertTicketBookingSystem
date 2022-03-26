import store from "../../store";
import * as actionCreators from "../../actionCreators";
import { Link } from 'react-router-dom';

function UserNamePanel() {
    return (
        <div id="userMenu" className="header_element">
            <div id="userName" className={store.getState().user.isAdmin ? "adminPanel" : "userPanel"}>
                <p id="userNameText" className="header_text">{store.getState().user.name}</p>
            </div>
            <div id="userPopup">
                <Link to="/personalization">
                    <div className="popup_button" >
                        Персонализ.
                    </div>
                </Link>
                <div className="popup_button">
                    Поиск
                </div>
                <div className="popup_button" onClick={logOut}>
                    Выйти
                </div>
            </div>
        </div>)
}

export default UserNamePanel;

function logOut() {
    store.dispatch(actionCreators.logOutThunkActionCreator());
}