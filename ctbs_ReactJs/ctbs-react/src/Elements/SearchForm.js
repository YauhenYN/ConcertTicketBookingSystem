function SearchForm() {
    return (
        <form id="searchForm">
            <input id="search" value="Поиск" className="header_element" type="search" />
            <input id="submit_button" className="header_element" type="submit" value="Найти" />
        </form>
    );
}
export default SearchForm;