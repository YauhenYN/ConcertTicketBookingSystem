import { Link } from "react-router-dom";
import store from "../../store";
import * as actionCreators from "../../actionCreators"; 



function SearchForm() {
    return (
        <div id="searchForm">
            <input id="search" value={store.getState().search.performer} onChange={event => store.dispatch(actionCreators.UpdateSearchPerformerActionCreator(event.target.value))} className="header_element" type="search" />
            <Link to="/search" state={{ performer: store.getState().search.performer }} id="searchButtonLink">
                <div className="header_element header_button">
                    <p className="header_search header_text">Найти</p>
                </div>
            </Link>
        </div>
    );
}
export default SearchForm;