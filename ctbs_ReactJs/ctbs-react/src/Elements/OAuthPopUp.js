import * as conf from "../configuration";

function OAuthPopUp() {
    return (
        <div id="login_popup">
            <a href={conf.googleLink}><input className="popup_button" id="google_button" type="button" value="Google" /></a>
            <a href={conf.facebookLink}><input className="popup_button" id="facebook_button" type="button" value="Facebook" /> </a>
            <a href={conf.microsoftLink}><input className="popup_button" id="microsoft_button" type="button" value="Microsoft" /></a>
        </div>
    );
}
export default OAuthPopUp;