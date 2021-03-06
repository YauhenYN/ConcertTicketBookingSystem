import './Main.css';
import Button from '../../../CommonElements/Button';
import ConcertsList from './ConcertsList';
import { useEffect, useState } from "react";
import * as actionCreators from "../../../actionCreators";
import store from "../../../store";
import Loading from '../Loading';
import * as configuration from '../../../configuration';



function Main() {
  const [isLoading, setIsLoading] = useState(true);
  const [concerts, setConcerts] = useState([]);
  const [actualConcerts, setActualConcerts] = useState([]);
  useEffect(() => {
    async function dispatches(){
      await store.dispatch(actionCreators.GetManyLightConcertsActionCreator(0, 10, null, null, null, null, null, true, null, null, null, null, null, null, 1)).then((result) => {
        setConcerts(configuration.ConcertsFilter(result.data.concerts));
      });
      await store.dispatch(actionCreators.GetManyLightConcertsActionCreator(0, 10, null, null, null, null, null, true, null, null, null, null, null, null, 2)).then((result) => {
        setActualConcerts(configuration.ConcertsFilter(result.data.concerts));
      });
    }
    dispatches().then(() => {
      setIsLoading(false);
    }).catch(() => {
      setIsLoading(false);
    });
  }, [])
  return (
    <div className="element-common">
      {isLoading ? <Loading /> : <>
        <h3 className="centerHeaderText">Предлагаем авторизироваться</h3>
        <div className="authorizationLine">
          <a className="authorizationLink" href={configuration.googleLink}>
            <Button text="Google" />
          </a>
          <a className="authorizationLink" href={configuration.facebookLink}>
            <Button text="Facebook" />
          </a>
          <a className="authorizationLink" href={configuration.microsoftLink}>
            <Button text="Microsoft" />
          </a>
        </div>
        <div className="centerHeaderText"></div>
        <h3 className="centerHeaderText">Новинки</h3>
        <ConcertsList concerts={concerts} />
        <div className="centerHeaderText"></div>
        <h3 className="centerHeaderText">Актуальные сейчас</h3>
        <ConcertsList concerts={actualConcerts} />
      </>}
    </div>);
}

export default Main;