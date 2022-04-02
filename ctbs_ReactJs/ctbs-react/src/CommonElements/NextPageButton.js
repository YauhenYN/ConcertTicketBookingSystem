import './Button.css';
import './NextPageButton.css';

function NextPageButton(props) {
    return (
        <div className = "button nextPageButton" onClick={props.onClick}/>
    );
  }

  export default NextPageButton;