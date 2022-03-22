import { Link } from 'react-router-dom';

function Footer() {
    return (
        <div id="footer" className="element-common">
            <b className="year">2022</b>
            <b className="bywho">By me</b>
            <Link className="privacy" to="/privacy">Privacy</Link>
        </div>
    );
}
export default Footer;
