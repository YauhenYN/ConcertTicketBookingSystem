import store from "../store";
import React, { useEffect, useState } from 'react';
import * as conf from '../configuration';
import * as thunks from '../thunkActionCreators';
import * as actionCreators from "../actionCreators";

function UserNamePanel() {
    const [userInfo, setUserInfo] = useState({
        name: '',
        isAdmin: false
    });
    useEffect(() => {
        store.dispatch(thunks.GetUserInfoThunkAction(conf.cookies.get('AccessToken'), () => {
            setUserInfo({ name: store.getState().user.name });
        }));
    }, []);
    return (
        <div id = "userMenu" className = "header_element" onMouseEnter={openForm} onMouseLeave={closeForm}>
            <div id="userName" className = {userInfo.isAdmin ? "adminPanel" : "userPanel"}>
                <p id="userNameText">{userInfo.name}</p>
            </div>
            <div id="userPopup">
                <input className = "popup_button" type="button" value="Персонализ." />
                <input type = "button" className = "popup_button" onClick={logOut} value="Выйти" />
            </div>
        </div>
    );
}
export default UserNamePanel;

function openForm() {
    document.getElementById("userPopup").style.display = "flex";
};
function closeForm() {
    document.getElementById("userPopup").style.display = "none";
};

function logOut(){
    store.dispatch(actionCreators.logOutAction());
}