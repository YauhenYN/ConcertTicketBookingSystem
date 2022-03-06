import * as conf from "../configuration";

function OAuthPopUp() {
    return (
        <div id="login_popup">
            <a href={conf.googleLink}><input id="google_button" type="button" value="Google" /></a>
            <a href={conf.facebookLink}><input id="facebook_button" type="button" value="Facebook" /> </a>
            <a href={conf.microsoftLink}><input id="microsoft_button" type="button" value="Microsoft" /></a>
        </div>
    );
}
export default OAuthPopUp;