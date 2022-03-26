import './Input.css';

function EmailInput(props) {
    return (
        <input required className = "Input" type="email" value = {props.value} onChange = {props.onChange}></input>
    );
}
export default EmailInput;