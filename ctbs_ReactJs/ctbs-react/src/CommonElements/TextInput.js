import './Input.css';

function TextInput(props) {
    return (
        <input required className = "Input" type="text" value = {props.value} onChange = {props.onChange} minLength = {props.minLength} maxLength = {props.maxLength}></input>
    );
}
export default TextInput;