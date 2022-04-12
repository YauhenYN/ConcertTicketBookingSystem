import { Link } from "react-router-dom";
import TextOnlyModalWindow from "./CommonElements/TextOnlyModalWindow";

const nextLineLink = (text) => <Link to={"/"} className="nextLineLink" onClick={closeModalWindow}>{text}</Link>;

function PaymentSuccess() {
    return <TextOnlyModalWindow text={<>Билет отправлен на вашу почту, также он добавлен в список билетов на вашей странице персонализации{nextLineLink("Перейти на страницу приветствия")}</>} />
}
export default PaymentSuccess;

function closeModalWindow() {
    document.querySelector("body").style.overflow = "auto";
}