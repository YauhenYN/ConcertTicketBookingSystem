import './Main.css';
import Button from '../../../CommonElements/Button';
import NewestConcertsList from './NewestConcertsList';

function Main() {
  return (
    <div className="element-common">
      <h3 className = "centerHeaderText">Предлагаем авторизироваться</h3>
      <div className="authorizationLine">
        <a className = "authorizationLink" href="https://localhost:44345/Authentication/OAuth/Google/Redirect">
          <Button text="Google" />
        </a>
        <a className = "authorizationLink" href="https://localhost:44345/Authentication/OAuth/Facebook/Redirect">
          <Button text="Facebook" />
        </a>
        <a className = "authorizationLink" href="https://localhost:44345/Authentication/OAuth/Microsoft/Redirect">
          <Button text="Microsoft" />
        </a>
      </div>
      <h3 className = "centerHeaderText"></h3>
      <h3 className = "centerHeaderText">Новинки</h3>
      <NewestConcertsList/>
    </div>
  );
}

export default Main;