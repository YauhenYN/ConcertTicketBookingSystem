import './Input.css';

function TimeInput(props) {
    return (
        <input required className = "Input" type="time" value = {props.value} onChange = {props.onChange} min = {props.min} max = {props.max}></input>
    );
}
export default TimeInput;