import * as actionTypes from "./actionTypes";
import * as conf from './configuration';
import axios from 'axios';
import jwt from 'jwt-decode'

let accessToken;

export const RefreshCodeThunkActionСreator = () => {
    return async function fetchTokenThunk(dispatch) {
        return axios.post(conf.apiLink + conf.refreshAddition, {
            crossDomain: true,
            xhrFields: { withCredentials: true }
        }, {
            withCredentials: true
        }).then((result) => {
            accessToken = result.data;
            setTimeout(() => {
                dispatch(RefreshCodeThunkActionСreator());
            }, new Date(jwt(result.data).exp * 1000) - new Date());
        });
    }
};
export const GetUserInfoThunkActionCreator = () => {
    return async function fetchTokenThunk(dispatch) {
        return axios.get(conf.apiLink + conf.userInfoAddition, {
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
        }).catch(error => {
            dispatch(getUserInfoFailureActionCreator(error.message));
        });
    }
};

export const EmptyStateActionCreator = () => {
    return { type: actionTypes.EmptyState }
}

export const logInThunkActionCreator = () => {
    return async function checkLogInThunk(dispatch) {
        try {
            await dispatch(RefreshCodeThunkActionСreator());
            await dispatch(GetUserInfoThunkActionCreator());
            await dispatch({
                type: actionTypes.LogIn,
                isLoggedIn: true
            });
        }
        catch (error) {
            await dispatch({
                type: actionTypes.LogIn,
                isLoggedIn: false
            });
        };
    }
};

export const confirmCookiesThunkActionCreator = () => {
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

export const logOutThunkActionCreator = () => {
    return async function LogOutThunk(dispatch) {
        await axios.post(conf.apiLink + conf.removeCookiesAddition, {}, {
            withCredentials: true,
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        }).then(() => {
            window.location.replace(window.location.origin);
            dispatch({
                type: actionTypes.LogOut,
            })
        });
    }
};

export const getUserInfoFailureActionCreator = (error) => {
    return {
        type: actionTypes.GetUserInfoFailure,
        error: error
    }
}

export const UpdateBirthDateThunkActionCreator = (birthYear) => {
    return async function updateBirthDateThunk(dispatch) {
        try {
            await axios.post(conf.apiLink + conf.updateBirthDateAddition, { birthYear: birthYear }, {
                headers: {
                    'Authorization': 'Bearer ' + accessToken
                }
            });
            dispatch({
                type: actionTypes.UpdateBirthDate,
                newBirthDate: birthYear
            })
            document.querySelector("body").style.overflow = "auto";
        }
        catch {

        }
    }
}
export const CancelUpdateBirthDateActionCreator = () => {
    document.querySelector("body").style.overflow = "auto";
    return {
        type: actionTypes.UpdateBirthDate
    }
}

export const UpdateSearchPerformerActionCreator = (performer) => {
    return {
        type: actionTypes.UpdateSearchPerformer,
        performer: performer
    }
}

export const GetActionsActionCreator = () => {
    return async function GetActionsThunk() {
        const get = await axios.get(conf.apiLink + conf.actionsManyAddition, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
        return get;
    }
}
export const UpdateNameActionCreator = (name) => {
    return async function UpdateNameThunk(dispatch) {
        try {
            await axios.post(conf.apiLink + conf.updateNameAddition, { newName: name }, {
                headers: {
                    'Authorization': 'Bearer ' + accessToken
                }
            });
            dispatch({
                type: actionTypes.UpdateName,
                name: name
            });
        }
        catch { }
    }
}
export const ActivatePromoCodeActionCreator = (promoCode) => {
    return async function ActivatePromoCodeThunk(dispatch) {
        await axios.post(conf.apiLink + conf.activatePromoCodeAddition, { code: promoCode }, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}
export const GetPromoCodeByIdThunkActionCreator = (promoCodeId) => {
    return async function fetchTokenThunk(dispatch) {
        return axios.get(conf.apiLink + conf.getOnePromoCodeByIdAddition + "?promoCodeId=" + promoCodeId, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
};
export const GetFullConcertActionCreator = (concertId) => {
    return async function fetchTokenThunk() {
        return axios.get(conf.apiLink + "/Concerts/" + concertId, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
};
export const UpdateEmailActionCreator = (email) => {
    return async function UpdateEmailThunk() {
        await axios.post(conf.apiLink + conf.updateEmailAddition, { newEmail: email }, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}
export const GiveAdminRightsActionCreator = (id) => {
    return async function GiveAdminRightsThunk(dispatch) {
        await axios.post(conf.apiLink + conf.giveRightsAddition + "?id=" + id, {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}
export const GetManyPromocodesActionCreator = (isActiveFlag, count) => {
    return async function GetManyPromoCodesThunk(dispatch) {
        return await axios.get(conf.apiLink + conf.getManyPromoCodesAddition, {
            params: {
                isActiveFlag: isActiveFlag,
                count: count
            },
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}
export const GetManyLightConcertsActionCreator = (nextPage, neededCount, byConcertType, byPerformer, 
    untilPrice, fromPrice, byUserId, byActivity, byConcertName, byVoiceType, byHeadLiner, dateFrom, dateUntil, byCompositor) => {
    return async function GetManyPromoCodesThunk(dispatch) {
        return await axios.get(conf.apiLink + conf.getManyLightConcerts, {
            params: {
                nextPage: nextPage,
                neededCount: neededCount,
                byActivity: byActivity,
                byConcertType: byConcertType,//0,1,2
                byPerformer: byPerformer,
                untilPrice: untilPrice,
                fromPrice: fromPrice,
                byUserId: byUserId,
                byConcertName: byConcertName,
                byVoiceType: byVoiceType,
                byHeadLiner: byHeadLiner,
                dateFrom: dateFrom,
                dateUntil: dateUntil,
                byCompositor: byCompositor
            },
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const GetManyTicketsActionCreator = (nextPage, byUserId, ByConcertId, neededCount) => {
    return async function GetManyPromoCodesThunk() {
        return await axios.get(conf.apiLink + conf.getTickets + "/many", {
            params: {
                pageNumber: nextPage,
                byUserId: byUserId,
                ByConcertId: ByConcertId,
                neededCount: neededCount
            },
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const GetTicketByTicketIdActionCreator = (ticketId) => {
    return async function GetManyPromoCodesThunk() {
        return await axios.get(conf.apiLink + conf.getTickets + "/" + ticketId, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const TakeAdminRightsActionCreator = (id) => {
    return async function TakeAdminRightsThunk(dispatch) {
        await axios.post(conf.apiLink + conf.takeRightsAddition + "?id=" + id, {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}
export const ActivatePromoCodeInListActionCreator = (id) => {
    return async function TakeAdminRightsThunk() {
        return await axios.post(conf.apiLink + "/PromoCodes/" + id + "/Activate", {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const ActivateConcertInListActionCreator = (id) => {
    return async function TakeAdminRightsThunk() {
        return await axios.post(conf.apiLink + "/Concerts/" + id + "/Activate", {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const DeactivatePromoCodeInListActionCreator = (id) => {
    return async function DeactivatePromoCodeInListThunk() {
        return await axios.post(conf.apiLink + "/PromoCodes/" + id + "/Deactivate", {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const MarkTicketActionCreator = (id) => {
    return async function MarkTicketThunk() {
        return await axios.post(conf.apiLink + "/Tickets/" + id + "/Mark", {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const UnmarkTicketActionCreator = (id) => {
    return async function UnmarkTicketThunk() {
        return await axios.post(conf.apiLink + "/Tickets/" + id + "/Unmark", {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}


export const DeactivateConcertInListActionCreator = (id) => {
    return async function TakeAdminRightsThunk() {
        return await axios.post(conf.apiLink + "/Concerts/" + id + "/Deactivate", {}, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const AddPromoCodeActionCreator = (uniqueCode, discount, onCount) => {
    return async function AddPromoCodeThunk(dispatch) {
        await axios.post(conf.apiLink + conf.addPromoCodeAddition, {
            uniqueCode: uniqueCode,
            discount: discount,
            isActiveFlag: true,
            onCount: onCount
        }, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}

export const AddConcertActionCreator = (isActiveFlag, imageType, image, cost, totalCount, performer, concertDate, latitude, longitude, concertType, classicConcertInfo, openAirConcertInfo, partyConcertInfo) => {
    return async function AddPromoCodeThunk() {
        return await axios.post(conf.apiLink + conf.addConcert, {
            isActiveFlag: isActiveFlag,
            imageType: imageType,
            image: image,
            cost: cost,
            totalCount: totalCount,
            performer: performer,
            concertDate: concertDate,
            latitude: latitude,
            longitude: longitude,
            concertType: concertType, //0/1/2
            classicConcertInfo: classicConcertInfo === null ? null : {
                voiceType: classicConcertInfo.voiceType,
                concertName: classicConcertInfo.concertName,
                compositor: classicConcertInfo.compositor
            },
            openAirConcertInfo: openAirConcertInfo === null ? null : {
                route: openAirConcertInfo.route,
                headLiner: openAirConcertInfo.headLiner
            },
            partyConcertInfo: partyConcertInfo === null ? null : {
                censure: partyConcertInfo.censure
            }
        }, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}
export const AddImageActionCreator = (imageType, image, concertId) => {
    return async function AddImageThunk() {
        await axios.post(conf.apiLink + conf.addImageAddition, {
            imageType: imageType,
            image: image,
            concertId: concertId
        }, {
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }
}
export const BuyTicketActionCreator = (count, concertId) => {
    return async function AddImageThunk() {
        await axios.post(conf.apiLink + "/Concerts/" + concertId + "/Buy/PayPal", {
            count: count,
        }, {
            headers: {
                'Authorization': 'Bearer ' + accessToken,
            }
        }).then(result => {
            window.location = result.data
        });
    }
}