import './DateInput.css';

function DateInput(props) {
    return (
        <input required className = "dateInput" type="date" value = {props.value} onChange = {props.onChange} min = {props.min} max = {props.max}></input>
    );
}
export default DateInput;
