import OAuthPopUp from "./OAuthPopUp";
import SearchForm from "./SearchForm";
import store from "../store";

function Header() {
    return (
        <div id="header_color">
            <img id="header_image" className="header_element" src="icon.png" alt="icon" />
            <SearchForm />
            {!store.getState().payload.isLoggedIn && <div id = "login_form">
                <input id="login_button" className="header_element" type="button" onClick={openCloseForm} value="Войти" />
                <OAuthPopUp />
            </div>}
            {store.getState().payload.isLoggedIn && <input id="personalization_button" className="header_element" type="button" value="Персонализация" />}
            {store.getState().payload.isLoggedIn && <input id="logout" className="header_element" type="button" value="Выйти" />}
        </div>
    );
}
function openCloseForm() {
    let display = document.getElementById("login_popup").style.display;
    if (display === "none" || display === "") document.getElementById("login_popup").style.display = "flex";
    else document.getElementById("login_popup").style.display = "none";
};
export default Header;