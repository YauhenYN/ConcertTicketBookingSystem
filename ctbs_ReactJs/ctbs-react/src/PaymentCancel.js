import { Link } from "react-router-dom";
import TextOnlyModalWindow from "./CommonElements/TextOnlyModalWindow";

const nextLineLink = (text) => <Link to={"/"} className = "nextLineLink" onClick={closeModalWindow}>{text}</Link>;

function PaymentCancel(){
    return <TextOnlyModalWindow text = {<>Покупка билета отменена{nextLineLink("Перейти на страницу приветствия")}</>}/>
}
export default PaymentCancel;

function closeModalWindow() {
    document.querySelector("body").style.overflow = "auto";
}