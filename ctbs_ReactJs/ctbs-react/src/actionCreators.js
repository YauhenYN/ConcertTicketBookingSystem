import * as actionTypes from "./actionTypes";
import * as conf from './configuration';
import axios from 'axios';
import jwt from 'jwt-decode'

let accessToken;

export const RefreshCodeThunkActionСreator = () => {
    return async function fetchTokenThunk(dispatch) {
        return axios.post(conf.apiLink + conf.refreshAddition, {}, {
            withCredentials: true
        }).then((result) => {
            accessToken = result.data;
            setTimeout(() => {
                dispatch(RefreshCodeThunkActionСreator());
            }, new Date(jwt(result.data).exp));
        });
    }
};
export const GetUserInfoThunkActionCreator = () => {
    return async function fetchTokenThunk(dispatch) {
        return axios.get(conf.apiLink + conf.userInfoAddition, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        }).then((result) => {
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
        }).catch(error => {
            dispatch(getUserInfoFailureActionCreator(error.message));
        });
    }
};

export const EmptyStateActionCreator = () => {
    return { type: actionTypes.EmptyState }
}

export const logInThunkActionCreator = () => {
    return async function checkLogInThunk(dispatch) {
        try {
            await dispatch(RefreshCodeThunkActionСreator());
            await dispatch(GetUserInfoThunkActionCreator());
            await dispatch({
                type: actionTypes.LogIn,
                isLoggedIn: true
            });
        }
        catch (error) {
            await dispatch({
                type: actionTypes.LogIn,
                isLoggedIn: false
            });
        };
    }
};

export const confirmCookiesThunkActionCreator = () => {
    return async function confirmCookiesThunk(dispatch) {
        try {
            await axios.post(conf.apiLink + conf.cookieConfirmationAddition, {}, {
                headers: {
                    'Authorization': 'Bearer ' + accessToken
                }
            });
            dispatch({
                type: actionTypes.ConfirmCookies
            })
        }
        catch {

        }
    }
}

export const logOutThunkActionCreator = () => {
    return async function LogOutThunk(dispatch) {
        await axios.post(conf.apiLink + conf.removeCookiesAddition, {}, {
            withCredentials: true,
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        }).then(() => {
            window.location.reload(false);
            dispatch({
                type: actionTypes.LogOut,
            })
        });
    }
};

export const getUserInfoFailureActionCreator = (error) => {
    return {
        type: actionTypes.GetUserInfoFailure,
        error: error
    }
}

export const UpdateBirthDateThunkActionCreator = (birthYear) => {
    return async function updateBirthDateThunk(dispatch) {
        try {
            await axios.post(conf.apiLink + conf.updateBirthDateAddition, { birthYear: birthYear }, {
                headers: {
                    'Authorization': 'Bearer ' + accessToken
                }
            });
            dispatch({
                type: actionTypes.UpdateBirthDate
            })
            document.querySelector("body").style.overflow = "auto";
        }
        catch {

        }
    }
}
export const CancelUpdateBirthDateActionCreator = () => {
    document.querySelector("body").style.overflow = "auto";
    return {
        type: actionTypes.UpdateBirthDate
    }
}
export const GetActionsActionCreator = () => {
    return async function GetActionsThunk() {
        const get = await axios.get(conf.apiLink + conf.actionsManyAddition, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
        return get;
    }
}