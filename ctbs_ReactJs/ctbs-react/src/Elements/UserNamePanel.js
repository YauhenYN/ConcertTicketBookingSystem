import store from "../store";
import React, { useEffect, } from 'react';
import * as thunks from '../thunkActionCreators';
import * as actionCreators from "../actionCreators";
import { connect } from "react-redux";

function UserNamePanel({ state, getUserInfo }) {
    useEffect(() => {
        getUserInfo();
    }, [getUserInfo]);
    return (
        state.isLoading > 0 ? (
            <p>LOADING</p>
        ) : (
            <div id="userMenu" className="header_element">
                <div id="userName" className={state.user.isAdmin ? "adminPanel" : "userPanel"}>
                    <p id="userNameText">{state.user.name}</p>
                </div>
                <div id="userPopup">
                    <input className="popup_button" type="button" value="Персонализ." />
                    <input type="button" className="popup_button" onClick={logOut} value="Выйти" />
                </div>
            </div>)
    );
}

const mapStateToProps = state => {
    return {
        user: state.user,
        isLoading: state.loading
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getUserInfo: dispatch(thunks.GetUserInfoThunkAction())
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(UserNamePanel);

function logOut() {
    store.dispatch(actionCreators.logOutAction());
}