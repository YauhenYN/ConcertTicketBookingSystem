import * as actions from "./actionTypes";

export const loggedInAction = () => {
    return {
        type: actions.LoggedIn,
    }
};
export const loggedOutAction = () => {
    return {
        type: actions.LoggedOut,
    }
};