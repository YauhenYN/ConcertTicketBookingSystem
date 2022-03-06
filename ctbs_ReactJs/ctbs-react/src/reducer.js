import * as actions from "./actionTypes"

export default function reducer(state, action) {
    if (action.type === actions.LoggedIn) {
        return {
            ...state,
            payload: {
                isLoggedIn: true
            }
        }
    }
    else if(action.type === actions.LoggedOut){
        return {
            ...state,
            payload: {
                isLoggedIn: false
            }
        }
    }
};