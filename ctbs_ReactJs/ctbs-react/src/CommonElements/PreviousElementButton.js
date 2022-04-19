import './Button.css';
import './PreviousElementButton.css';

function PreviousElementButton(props) {
    return (
        <div className = "button previousElementButton" onClick={props.onClick}/>
    );
  }

  export default PreviousElementButton;