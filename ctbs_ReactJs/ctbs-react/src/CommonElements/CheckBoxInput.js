import './CheckBox.css';

function CheckBoxInput(props) {
    return (
        <div className="outOfCheckBox">
            <input className="CheckBox" type="checkbox" checked={props.isChecked} onChange={props.onChange} />
            <label className='CheckBoxLabel'>{props.text}</label>
        </div>
    )
}
export default CheckBoxInput;