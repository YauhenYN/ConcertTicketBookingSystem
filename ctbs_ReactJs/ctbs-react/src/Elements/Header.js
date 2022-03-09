import OAuthPopUp from "./OAuthPopUp";
import SearchForm from "./SearchForm";
import store from "../store";
import UserNamePanel from "./UserNamePanel";


function Header() {
    return (
        <div id="header_color">
            <img id="header_image" className="header_element" src="icon.png" alt="icon" />
            <SearchForm />
            {!store.getState().isLoggedIn && <div id = "login_form">
                <input id="login_button" className="header_element" type="button" onClick={openCloseForm} value="Войти" />
                <OAuthPopUp />
            </div>}
            {store.getState().isLoggedIn && <>
            <UserNamePanel/>
            </>}
        </div>
    );
}
function openCloseForm() {
    let display = document.getElementById("login_popup").style.display;
    if (display === "none" || display === "") document.getElementById("login_popup").style.display = "flex";
    else document.getElementById("login_popup").style.display = "none";
};
export default Header;