function SearchForm() {
    return (
        <div id="searchForm">
            <input id="search" defaultValue = "Поиск" className="header_element" type="search" />
            <div className="header_element header_button" onClick={onClickSearch}>
                <p className = "header_search header_text">Найти</p> 
            </div>
        </div>
    );
}

const onClickSearch = () => {

}
export default SearchForm;