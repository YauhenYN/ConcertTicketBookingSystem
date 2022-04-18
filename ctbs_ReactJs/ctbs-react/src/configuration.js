import Cookies from "universal-cookie/es6";

export const googleLink = "https://localhost:44345/Authentication/OAuth/Google/Redirect";
export const facebookLink = "https://localhost:44345/Authentication/OAuth/Facebook/Redirect";
export const microsoftLink = "https://localhost:44345/Authentication/OAuth/Microsoft/Redirect";
export const firstInterval = 3000;


export const cookies = new Cookies();


export const apiLink = "https://localhost:44345";
export const refreshAddition = "/Authentication/Refresh";
export const userInfoAddition = "/Personalization/UserInfo";

export const toLocaleDate = (date) => {
    let z = date.getTimezoneOffset() * 60 * 1000;
    return new Date(date - z);
}
export const toCommonDateFormat = (date) => {
    return date.slice(0, 19).replace(/-/g, "/").replace("T", " ");
}
export const cookieConfirmationAddition = "/Personalization/ConfirmCookies";
export const updateBirthDateAddition = "/Personalization/UpdateBirthYear";
export const updateNameAddition = "/Personalization/UpdateName";
export const updateEmailAddition = "/Personalization/UpdateEmail";
export const activatePromoCodeAddition = "/Personalization/ActivatePromocode";


export const giveRightsAddition = "/Administration/AddAdmin";
export const takeRightsAddition = "/Administration/RemoveAdmin";

export const removeCookiesAddition = "/Authentication/LogOut";

export const actionsManyAddition = "/Actions/many";

export const addImageAddition = "/Images";

export const getOnePromoCodeByIdAddition = "/PromoCodes/id";
export const getManyPromoCodesAddition = "/PromoCodes/many";
export const addPromoCodeAddition = "/PromoCodes";


export const addConcert = "/Concerts"
export const getManyLightConcerts = "/Concerts/many/light"




export const getTickets = "/Tickets"