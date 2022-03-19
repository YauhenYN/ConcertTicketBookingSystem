

function CookieModalWindow() {
    return (<div className="modal">
            <div id="cookiesModal">
                <p>Если вы согласны с использованием нами Cookies, нажмите кнопку</p>
                <input type="button" id="cookieButton" value="Согласен" onClick = {cookieButtonOnClick}/>
            </div>
        </div>
    );
}

function cookieButtonOnClick(){
    document.getElementById("cookieButton").style.display = "flex";
}

export default CookieModalWindow;
