import * as conf from "../../configuration";

function OAuthPopUp() {
    return (
        <div id="login_popup">
            <a href={conf.googleLink}><div className="popup_button" id="google_button" onClick = {OAuthOnClick}>
                Google
                </div></a>
            <a href={conf.facebookLink}><div className="popup_button" id="facebook_button" onClick = {OAuthOnClick}>
                Facebook
                </div> </a>
            <a href={conf.microsoftLink}><div className="popup_button" id="microsoft_button" onClick = {OAuthOnClick}>
                Microsoft
                </div></a>
        </div>
    );
}
function OAuthOnClick() {
    localStorage.setItem("lastLocation", window.location);
}
export default OAuthPopUp;