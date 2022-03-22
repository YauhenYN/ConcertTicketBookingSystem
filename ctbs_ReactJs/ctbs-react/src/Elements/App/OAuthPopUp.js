import * as conf from "../../configuration";

function OAuthPopUp() {
    return (
        <div id="login_popup">
            <a href={conf.googleLink}><div className="popup_button" id="google_button" >
                <p className="header_text">Google</p>
                </div></a>
            <a href={conf.facebookLink}><div className="popup_button" id="facebook_button" >
                <p className="header_text">Facebook</p>
                </div> </a>
            <a href={conf.microsoftLink}><div className="popup_button" id="microsoft_button" >
                <p className="header_text">Microsoft</p>
                </div></a>
        </div>
    );
}
export default OAuthPopUp;