import './Button.css';

function Button(props) {
    return (
        <input type="button" className = "button" onClick={props.onClick} value = {props.text}/>
    );
  }

  export default Button;