function SearchForm() {
    return (
        <div id="searchForm">
            <input id="search" defaultValue = "Поиск" className="header_element" type="search" />
            <input id="submit_button" className="header_element" type="button" value="Найти" onClick={onClickSearch}/>
        </div>
    );
}

const onClickSearch = () => {

}
export default SearchForm;