import OAuthPopUp from "./OAuthPopUp";
import SearchForm from "./SearchForm";
import store from "../../store";
import UserNamePanel from "./UserNamePanel";
import { Link } from "react-router-dom";


function Header() {
    return (
        <div id="header_color">
            <Link to="/">
                <img id="header_image" className="header_element" src="icon.png" alt="icon" />
            </Link>
            <SearchForm />
            {!store.getState().isLoggedIn && <div id="login_form">
                <div id="login_button" className="header_element">
                    <p>Войти</p>
                </div>
                <OAuthPopUp />
            </div>}
            {store.getState().isLoggedIn && <>
                <UserNamePanel />
            </>}
        </div>
    );
}
export default Header;