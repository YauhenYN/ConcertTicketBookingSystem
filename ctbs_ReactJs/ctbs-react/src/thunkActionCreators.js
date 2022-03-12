import * as conf from './configuration';
import * as actionTypes from "./actionTypes"
import * as thunks from './thunkActionCreators';
import store from "./store";
import * as actionCreators from "./actionCreators"

export const RefreshCodeThunkAction = (refresh) => {
    return async function fetchTokenThunk(dispatch) {
        if (typeof conf.cookies.get('AccessToken') !== 'undefined') {
            fetch(conf.apiLink + conf.refreshAddition + '?refreshToken=' + refresh, {
                method: 'POST'
            })
                .then(res => res.json()).then((result) => {
                    setTimeout(() => {
                        store.dispatch(thunks.RefreshCodeThunkAction(conf.cookies.get('RefreshToken')));
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
export const GetUserInfoThunkAction = () => {
    return async function fetchTokenThunk(dispatch) {
        dispatch(actionCreators.loading);
        fetch(conf.apiLink + conf.userInfoAddition, {
            method: 'GET'
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
                dispatch(actionCreators.loaded);
            }).catch(error => {
                dispatch(actionCreators.getUserInfoFailure(error.message));
            });
    }
};