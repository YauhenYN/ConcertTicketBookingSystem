import './CheckBox.css';

function RadioInput(props) {
    return (
        <div className="outOfCheckBox">
            <label className='CheckBoxLabel'>
                {props.text}
                <input className="CheckBox" type="radio" value={props.value} onChange={props.onChange} checked = {props.checked} />
            </label>
        </div>
    )
}
export default RadioInput;