import './Button.css';
import './FileInput.css';


function FileInput(props) {
    return (<>
        <input className="FileInput" id = "fileLoader" type="file" accept = {props.accept} multiple = {props.multiple} onChange={props.onChange}></input>
        <label className = "button" htmlFor = "fileLoader">{props.text}</label></>
    );
}
export default FileInput;