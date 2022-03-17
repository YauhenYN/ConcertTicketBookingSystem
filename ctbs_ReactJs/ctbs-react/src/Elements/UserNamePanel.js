import store from "../store";
import React, { useEffect, } from 'react';
import { connect } from "react-redux";
import * as thunks from '../thunkActionCreators';
import * as actionCreators from "../actionCreators";

function UserNamePanel({ props, getUserInfo }) {
    useEffect(() => {
        getUserInfo();
    }, [getUserInfo]);
    console.log(props);
    return (
        props.isLoading !== 0 ? (
            <p>LOADING</p>
        ) : (
            <div id="userMenu" className="header_element">
                <div id="userName" className={props.user.isAdmin ? "adminPanel" : "userPanel"}>
                    <p id="userNameText">{props.user.name}</p>
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
        props: {
            user: state.user,
            isLoading: state.isLoading
        }
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getUserInfo: () => dispatch(thunks.GetUserInfoThunkAction())
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(UserNamePanel);

function logOut() {
    store.dispatch(actionCreators.logOutAction());
}