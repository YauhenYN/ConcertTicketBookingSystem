import './Input.css';

function NumberInput(props) {
    return (
        <input required className = "Input" type="number" value = {props.value} onChange = {props.onChange} min = {props.min} max = {props.max} step = {props.step}></input>
    );
}
export default NumberInput;