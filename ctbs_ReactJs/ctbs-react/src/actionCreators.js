import * as actionTypes from "./actionTypes";
import * as conf from './configuration';
import axios from 'axios';
import jwt from 'jwt-decode'


let accessToken;

export const RefreshCodeThunkAction = () => {
    return async function fetchTokenThunk(dispatch) {
        return axios.post(conf.apiLink + conf.refreshAddition, {}, {
            withCredentials: true
        }).then((result) => {
            accessToken = result.data;
            setTimeout(() => {
                dispatch(RefreshCodeThunkAction());
            }, new Date(jwt(result.data).exp));
        });
    }
};
export const GetUserInfoThunkAction = () => {
    return async function fetchTokenThunk(dispatch) {
        dispatch(loading());
        axios.get(conf.apiLink + conf.userInfoAddition, {
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
            dispatch(loaded());
        }).catch(error => {
            dispatch(getUserInfoFailure(error.message));
        });
    }
};

export const EmptyState = () => {
    return { type: actionTypes.EmptyState }
}

export const logInThunkAction = () => {
    return async function checkLogInThunk(dispatch) {
        dispatch(loading());
        try {
            await dispatch(RefreshCodeThunkAction())
            dispatch(loaded());
            dispatch(GetUserInfoThunkAction());
            dispatch({
                type: actionTypes.LogIn,
                isLoggedIn: true
            })
        }
        catch (error) {
            dispatch(loaded());
            dispatch({
                type: actionTypes.LogIn,
                isLoggedIn: false
            });
        };
    }
};

export const confirmCookiesThunkAction = () => {
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

export const logOutAction = () => {
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

export const getUserInfoFailure = (error) => {
    return {
        type: actionTypes.GetUserInfoFailure,
        error: error
    }
}

export const loaded = () => {
    return {
        type: actionTypes.Loaded
    }
}
export const loading = () => {
    return {
        type: actionTypes.Loading
    }
}
export const UpdateBirthDateThunkAction = (birthYear) => {
    return async function updateBirthDateThunk(dispatch) {
        try {
            await axios.post(conf.apiLink + conf.updateBirthDateAddition, { birthYear: birthYear}, {
                headers: {
                    'Authorization': 'Bearer ' + accessToken
                }
            });
            dispatch({
                type: actionTypes.UpdateBirthDate
            })
        }
        catch {
            window.location.reload(false);
        }
    }
} 
export const CancelUpdateBirthDateAction = () => {
    document.querySelector("body").style.overflow = "auto";
    return {
        type: actionTypes.UpdateBirthDate
    }
}