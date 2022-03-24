import store from "../../store";
import * as actionCreators from "../../actionCreators";
import './ModalWindow.css';
import Button from "../../CommonElements/Button";

function CookieModalWindow() {
    document.querySelector("body").style.overflow = "hidden";
    return (
        <div className="modal">
            <div id="cookiesModal" className="Modal">
                <strong className="modalWindowText">Если вы согласны с использованием нами Cookies, нажмите кнопку</strong>
                <Button text="Согласен" onClick={cookieButtonOnClick} />
            </div>
        </div>
    );
}

function cookieButtonOnClick() {
    store.dispatch(actionCreators.confirmCookiesThunkActionCreator());
}

export default CookieModalWindow;
