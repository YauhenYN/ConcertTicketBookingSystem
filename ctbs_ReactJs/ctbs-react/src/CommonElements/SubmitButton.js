import './Button.css';

function SubmitButton(props) {
    return (
        <input type = "submit" className = "button" value = {props.text} onClick = {props.onClick}/>
    );
  }

  export default SubmitButton;