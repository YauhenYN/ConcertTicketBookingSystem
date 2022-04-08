import '../Elements/App/ModalWindow.css';

function TextOnlyModalWindow(props) {
    document.querySelector("body").style.overflow = "hidden";
    return (
        <div className="modal">
            <div id="cookiesModal" className="Modal">
                <strong className="modalWindowText">{props.text}</strong>
            </div>
        </div>
    );
}

export default TextOnlyModalWindow;