import './Button.css';
import './NextElementButton.css';

function NextElementButton(props) {
    return (
        <div className = "button nextElementButton" onClick={props.onClick}/>
    );
  }

  export default NextElementButton;