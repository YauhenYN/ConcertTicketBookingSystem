import * as actionTypes from "./actionTypes"

export default function reducer(state, action) {
    switch (action.type) {
        case actionTypes.EmptyState:
            return {
                ...state,
                search: {
                    performer: ""
                }
            }
        case actionTypes.LogIn:
            return {
                ...state,
                isLoggedIn: action.isLoggedIn,
            }
        case actionTypes.RefreshCode:
            return {
                ...state
            }
        case actionTypes.GetUserInfo:
            return {
                ...state,
                user: {
                    userId: action.userId,
                    isAdmin: action.isAdmin,
                    birthDate: action.birthDate,
                    promoCodeId: action.promoCodeId,
                    name: action.name,
                    email: action.email,
                },
                cookieConfirmationFlag: action.cookieConfirmationFlag,
                error: ''
            }
        case actionTypes.ConfirmCookies:
            return {
                ...state,
                cookieConfirmationFlag: true
            }
        case actionTypes.GetUserInfoFailure:
            return {
                ...state,
                error: action.error
            }
        case actionTypes.LogOut:
            return {
                ...state,
            }
        case actionTypes.UpdateBirthDate:
            return {
                ...state,
                birthDateModalDisabled: true,
                user: {
                    ...state.user,
                    birthDate: action.newBirthDate,
                },
            }
        case actionTypes.UpdateName:
            return {
                ...state,
                user: {
                    ...state.user,
                    name: action.name
                },
            }
        case actionTypes.UpdateSearchPerformer:
            return {
                ...state,
                search: {
                    ...state.search,
                    performer: action.performer
                }
            }
    }
};
