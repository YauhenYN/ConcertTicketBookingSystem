import * as actionTypes from "./actionTypes"
import store from "./store";
import * as actions from './actionCreators';
import Cookies from 'universal-cookie';
import * as conf from './configuration';

const cookies = new Cookies();

export default function reducer(state, action) {
    if (action.type === actionTypes.LogIn) {
        if (cookies.get('AccessToken') === undefined) {
            return {
                ...state,
                payload: {
                    isLoggedIn: false
                }
            }
        }
        else {
            setInterval(() => {
                store.dispatch(actions.RefreshCodeAction());
            }, conf.refreshInterval);
            return {
                ...state,
                payload: {
                    isLoggedIn: true
                }
            }
        }
    }
    else if (action.type === actionTypes.RefreshCode) {
        //Запрос на получение кода
        //Обновить код




    }
};