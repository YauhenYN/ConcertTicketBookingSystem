import Cookies from "universal-cookie/es6";
export const apiLink = "https://localhost:44345"; 


export const googleLink = apiLink + "/Authentication/OAuth/Google/Redirect";
export const facebookLink = apiLink + "/Authentication/OAuth/Facebook/Redirect";
export const microsoftLink = apiLink + "/Authentication/OAuth/Microsoft/Redirect";
export const firstInterval = 3000;


export const SuccessPageAddition = "/Payment/Success";


export const cookies = new Cookies();
export const refreshAddition = "/Authentication/Refresh";
export const userInfoAddition = "/Personalization/UserInfo";

export const toLocaleDate = (date) => {
    let z = date.getTimezoneOffset() * 60 * 1000;
    return new Date(date - z);
}
export const toCommonDateFormat = (date) => {
    return date.slice(0, 19).replace(/-/g, "/").replace("T", " ");
}

export function ConcertsFilter(concerts){
    return concerts.filter(concert => concert.leftCount > 0);
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

export const getManyUsersBriefInfo = "Administration/Users/Many/Brief";


export const getTickets = "/Tickets"