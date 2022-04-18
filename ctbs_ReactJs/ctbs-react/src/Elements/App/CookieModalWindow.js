import store from "../../store";
import * as actionCreators from "../../actionCreators";
import './ModalWindow.css';
import Button from "../../CommonElements/Button";
import * as actionTypes from "../../actionTypes";

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
    if (store.getState().user) {
        store.dispatch(actionCreators.confirmCookiesThunkActionCreator());
    }
    else {
        localStorage.setItem("cookieModalWindowDisabled", true);
        store.dispatch({ type: actionTypes.ConfirmCookies });
    }
}

export default CookieModalWindow;
