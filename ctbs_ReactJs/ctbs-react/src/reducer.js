import * as actionTypes from "./actionTypes"
import store from "./store";
import * as thunks from './thunkActionCreators';
import Cookies from 'universal-cookie';
import * as conf from './configuration';

const cookies = new Cookies();

export default function reducer(state, action) {
    if (action.type === actionTypes.LogIn) {
        if (typeof cookies.get('AccessToken') === "undefined") {
            return {
                ...state,
                payload: {
                    isLoggedIn: false
                }
            }
        }
        else {
            setTimeout(() => {
                store.dispatch(thunks.RefreshCodeThunkAction(cookies.get('AccessToken'), cookies.get('RefreshToken')));
            }, conf.firstInterval);
            return {
                ...state,
                payload: {
                    isLoggedIn: true //а инфа пользователя?
                }
            }
        }
    }
    else if (action.type === actionTypes.RefreshCode) {
        cookies.set('AccessToken', action.accessToken, { path: '/', expires: new Date(action.accessTokenExpires)});
        cookies.set('RefreshToken', action.refreshToken, { path: '/', expires: new Date(action.refreshTokenExpires)});
        setTimeout(() => {
            store.dispatch(thunks.RefreshCodeThunkAction(cookies.get('AccessToken'), cookies.get('RefreshToken')));
        }, new Date(action.accessTokenExpires) - 5000);
    }
};