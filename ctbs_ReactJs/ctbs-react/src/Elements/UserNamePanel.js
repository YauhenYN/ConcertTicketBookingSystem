import store from "../store";
import * as actionCreators from "../actionCreators";

function UserNamePanel() {
    return (
        store.getState().isLoading !== 0 ? <></> : (
            <div id="userMenu" className="header_element">
                <div id="userName" className={store.getState().isAdmin ? "adminPanel" : "userPanel"}>
                    <p id="userNameText" className="header_text">{store.getState().user.name}</p>
                </div>
                <div id="userPopup">
                    <div className="popup_button" value="" >
                        <p className="header_text">Персонализ.</p>
                    </div>
                    <div className="popup_button" onClick={logOut} value="Выйти">
                        <p className="header_text">Выйти</p>
                    </div>
                </div>
            </div>)
    );
}

export default UserNamePanel;

function logOut() {
    store.dispatch(actionCreators.logOutAction());
}