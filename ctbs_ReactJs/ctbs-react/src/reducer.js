import * as actionTypes from "./actionTypes"

export default function reducer(state, action) {
    if (action.type === actionTypes.LogIn) { 
        return {
            ...state,
            isLoggedIn: action.isLoggedIn,
        }
    }
    else if (action.type === actionTypes.RefreshCode) {
        return{
            ...state,
        }
    }
    else if (action.type === actionTypes.GetUserInfo) {
        return {
            ...state,
            user: {
                userId: action.userId,
                isAdmin: action.isAdmin,
                birthDate: action.birthDate,
                promoCodeId: action.promoCodeId,
                name: action.name,
                email: action.email,
                cookieConfirmationFlag: action.cookieConfirmationFlag
            },
        }
    }
    else if (action.type === actionTypes.LogOut) {
        return{
            ...state,
        }
    }
};