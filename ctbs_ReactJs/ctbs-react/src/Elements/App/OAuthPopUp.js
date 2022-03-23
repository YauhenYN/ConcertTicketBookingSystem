import * as conf from "../../configuration";

function OAuthPopUp() {
    return (
        <div id="login_popup">
            <a href={conf.googleLink}><div className="popup_button" id="google_button" >
                Google
                </div></a>
            <a href={conf.facebookLink}><div className="popup_button" id="facebook_button" >
                Facebook
                </div> </a>
            <a href={conf.microsoftLink}><div className="popup_button" id="microsoft_button" >
                Microsoft
                </div></a>
        </div>
    );
}
export default OAuthPopUp;