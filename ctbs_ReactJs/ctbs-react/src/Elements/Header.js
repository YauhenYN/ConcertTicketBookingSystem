import OAuthPopUp from "./OAuthPopUp";
import SearchForm from "./SearchForm";

function Header() {
    return (
        <div id="header_color">
            <img id="header_image" className="header_element" src="icon.png" alt="icon" />
            <SearchForm/>
            <input id="login_button" className="header_element" type="button" onClick={openCloseForm} value="Войти" />
            <OAuthPopUp/>
            <input id="personalization_button" className="header_element" type="button" value="Персонализация" />
            <input id="logout" className="header_element" type="button" value="Выйти" />
            <div id="emptyElement"></div>
        </div>
    );
}
function openCloseForm() {
    let display = document.getElementById("login_popup").style.display;
    if (display === "none" || display === "") document.getElementById("login_popup").style.display = "block";
    else document.getElementById("login_popup").style.display = "none";
}
export default Header;