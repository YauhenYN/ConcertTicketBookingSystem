import * as actionTypes from "./actionTypes"

export default function reducer(state, action) {
    if(action.type === actionTypes.EmptyState){
        return {
            ...state
        }
    }
    if (action.type === actionTypes.LogIn) { 
        return {
            ...state,
            isLoggedIn: action.isLoggedIn,
        }
    }
    else if (action.type === actionTypes.RefreshCode) {
        return{
            ...state
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
            error: ''
        }
    }
    else if(action.type === actionTypes.ConfirmCookies){
        return {
            ...state,
            user: {
                userId: state.user.userId,
                isAdmin: state.user.isAdmin,
                birthDate: state.user.birthDate,
                promoCodeId: state.user.promoCodeId,
                name: state.user.name,
                email: state.user.email,
                cookieConfirmationFlag: true
            },
        }
    }
    else if(action.type === actionTypes.GetUserInfoFailure){
        return {
            ...state,
            error: action.error
        }
    }
    else if (action.type === actionTypes.LogOut) {
        return{
            ...state,
        }
    }
    else if (action.type === actionTypes.Loading) {
        return{
            ...state,
            isLoading: typeof state === 'undefined' || typeof state.isLoading === 'undefined' ? 1 : state.isLoading + 1
        }
    }
    else if(action.type === actionTypes.Loaded){
        return{
            ...state,
            isLoading: state.isLoading - 1
        }
    }
    else if(action.type === actionTypes.UpdateBirthDate){
        return{
            ...state,
            birthDateModalDisabled: true
        }
    }
};