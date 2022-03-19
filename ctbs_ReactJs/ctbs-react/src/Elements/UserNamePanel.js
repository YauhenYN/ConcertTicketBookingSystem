import store from "../store";
import * as actionCreators from "../actionCreators";

function UserNamePanel() {
    return (
        store.getState().isLoading !== 0 ? <></> : (
            <div id="userMenu" className="header_element">
                <div id="userName" className={store.getState().isAdmin ? "adminPanel" : "userPanel"}>
                    <p id="userNameText">{store.getState().user.name}</p>
                </div>
                <div id="userPopup">
                    <input className="popup_button" type="button" value="Персонализ." />
                    <input type="button" className="popup_button" onClick={logOut} value="Выйти" />
                </div>
            </div>)
    );
}

export default UserNamePanel;

function logOut() {
    store.dispatch(actionCreators.logOutAction());
}