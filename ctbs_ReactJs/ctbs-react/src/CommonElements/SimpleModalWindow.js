import '../Elements/App/ModalWindow.css';
import Button from ".//Button";

function SimpleModalWindow(props) {
    document.querySelector("body").style.overflow = "hidden";
    return (
        <div className="modal">
            <div id="cookiesModal" className="Modal">
                <strong className="modalWindowText">{props.text}</strong>
                <Button text={props.buttonText} onClick={props.onClick} />
            </div>
        </div>
    );
}

export default SimpleModalWindow;