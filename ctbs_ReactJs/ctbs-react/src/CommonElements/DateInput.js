import './Input.css';

function DateInput(props) {
    return (
        <input required className = "Input" type="date" value = {props.value} onChange = {props.onChange} min = {props.min} max = {props.max}></input>
    );
}
export default DateInput;
