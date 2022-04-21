import './Input.css';

function SelectInput(props) {
    return (
        <select required={props.required != null ? props.required : true} className="Input" value = {props.value} onChange={props.onChange}>
            {props.values.map((value) => {
                return <option key = {value} value={value}>{value}</option>
            })}
        </select>
    );
}
export default SelectInput;