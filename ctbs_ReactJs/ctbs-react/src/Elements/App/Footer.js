import { Link } from 'react-router-dom';

function Footer() {
    return (
        <div id="footer" className="element-common">
            <b className="year">2022</b>
            <b className="bywho">By  <a className = 'href' href='https://www.linkedin.com/in/eyaroshevich/' rel="noopener noreferrer" target="_blank">@eyaroshevich</a></b>
            <Link className='privacy href' to='/privacy'>Privacy</Link>
        </div>
    );
}
export default Footer;
