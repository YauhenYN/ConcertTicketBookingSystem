import * as actions from "./actionTypes";
import store from "./store";
import * as thunks from './thunkActionCreators';
import * as conf from './configuration';

export const logInAction = () => {
    if (typeof conf.cookies.get('AccessToken') === 'undefined' || conf.cookies.get('AccessToken') === 'undefined') {
        return {
            type: actions.LogIn,
            isLoggedIn: false
        }
    }
    else {
        setTimeout(() => {
            store.dispatch(thunks.RefreshCodeThunkAction(conf.cookies.get('AccessToken'), conf.cookies.get('RefreshToken')));
        }, conf.firstInterval);
        return {
            type: actions.LogIn,
            isLoggedIn: true
        }
    }
};
export const logOutAction = () => {
    conf.cookies.remove('AccessToken');
    conf.cookies.remove('RefreshToken');
    window.location.reload(false);
    return {
        type: actions.LogOut,
    }
};