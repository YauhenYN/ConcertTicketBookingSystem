import * as conf from './configuration';
import * as actionTypes from "./actionTypes"
import * as thunks from './thunkActionCreators';
import store from "./store";

const Headers = (access) => { return { 'Authorization': 'Bearer ' + access } };

export const RefreshCodeThunkAction = (access, refresh) => {
    return async function fetchTokenThunk(dispatch) {
        if (typeof conf.cookies.get('AccessToken') !== 'undefined') {
            fetch(conf.apiLink + conf.refreshAddition + '?refreshToken=' + refresh, {
                method: 'POST',
                headers: Headers(access)
            })
                .then(res => res.json()).then((result) => {
                    conf.cookies.set('AccessToken', result.accessToken, { path: '/', expires: new Date(result.expirationTime) });
                    conf.cookies.set('RefreshToken', result.refreshToken, { path: '/', expires: new Date(result.refreshExpirationTime) });
                    setTimeout(() => {
                        store.dispatch(thunks.RefreshCodeThunkAction(conf.cookies.get('AccessToken'), conf.cookies.get('RefreshToken')));
                    }, new Date(result.expirationTime) - new Date() - 5000);
                    if(result.accessToken !== undefined) dispatch({
                        type: actionTypes.RefreshCode,
                        accessToken: result.accessToken,
                        accessTokenExpires: result.expirationTime,
                        refreshToken: result.refreshToken,
                        refreshTokenExpires: result.refreshExpirationTime
                    })

                })
        }
        else {
            window.location.reload(false);
        }
    }
};
export const GetUserInfoThunkAction = (access, func) => {
    return async function fetchTokenThunk(dispatch) {
        fetch(conf.apiLink + conf.userInfoAddition, {
            method: 'GET',
            headers: Headers(access)
        }).then(res => res.json()).then((result) => {
                dispatch({
                    type: actionTypes.GetUserInfo,
                    userId: result.userId,
                    isAdmin: result.isAdmin,
                    birthDate: result.birthDate,
                    promoCodeId: result.promoCodeId,
                    name: result.name,
                    email: result.email,
                    cookieConfirmationFlag: result.cookieConfirmationFlag
                });
            }).then(func);
    }
};