import * as conf from './configuration';
import * as actionTypes from "./actionTypes"
import * as thunks from './thunkActionCreators';
import * as actionCreators from "./actionCreators"
import axios from 'axios';

const axiosParams = {
    crossDomain: true,
    withCredentials: true
};

export const RefreshCodeThunkAction = (refresh) => {
    return async function fetchTokenThunk(dispatch) {
        if (typeof conf.cookies.get('AccessToken') !== 'undefined') {
            axios.post(conf.apiLink + conf.refreshAddition + '?refreshToken=' + refresh, { withCredentials: true })
                .then((result) => {
                    //conf.cookies.set('AccessToken', result.accessToken, { path: '/', expires: new Date(result.expirationTime) });
                    //conf.cookies.set('RefreshToken', result.refreshToken, { path: '/', expires: new Date(result.refreshExpirationTime) });
                    console.log(result);
                    setTimeout(() => {
                        dispatch(thunks.RefreshCodeThunkAction(conf.cookies.get('RefreshToken')));
                    }, new Date(result.data.expirationTime) - new Date() - 5000);
                });
        }
        else {
            window.location.reload(false);
        }
    }
};
export const GetUserInfoThunkAction = () => {
    return async function fetchTokenThunk(dispatch) {
        dispatch(actionCreators.loading());
        axios.get(conf.apiLink + conf.userInfoAddition, axiosParams)
            .then((result) => {
                dispatch({
                    type: actionTypes.GetUserInfo,
                    userId: result.data.userId,
                    isAdmin: result.data.isAdmin,
                    birthDate: result.data.birthDate,
                    promoCodeId: result.data.promoCodeId,
                    name: result.data.name,
                    email: result.data.email,
                    cookieConfirmationFlag: result.data.cookieConfirmationFlag
                });
                dispatch(actionCreators.loaded());
            }).catch(error => {
                dispatch(actionCreators.getUserInfoFailure(error.message));
            });
    }
};