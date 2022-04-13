import * as actionTypes from "./actionTypes"

export default function reducer(state, action) {
    if(action.type === actionTypes.EmptyState){
        return {
            ...state, 
            search: {
                performer: ""
            }
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
                ...state.user,
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
    else if(action.type === actionTypes.UpdateBirthDate){
        return{
            ...state,
            birthDateModalDisabled: true,
            user: {
                ...state.user,
                birthDate: action.newBirthDate,
            },
        }
    }
    else if(action.type === actionTypes.UpdateName){
        return{
            ...state,
            user: {
                ...state.user,
                name: action.name
            },
        }
    }
    else if(action.type === actionTypes.UpdateSearchPerformer){
        return{
            ...state,
            search: {
                ...state.search,
                performer: action.performer
            }
        }
    }
};