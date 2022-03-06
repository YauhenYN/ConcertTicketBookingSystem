import * as actions from "./actionTypes";

export const logInAction = () => {
    return {
        type: actions.LogIn,
    }
};
export const RefreshCodeAction = () => {
    return {
        type: actions.RefreshCode,
    }
};