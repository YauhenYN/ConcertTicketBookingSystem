import Footer from './Footer';
import Header from './Header';
import CookieModalWindow from './CookieModalWindow';
import * as actionCreators from "../../actionCreators";
import React, { useEffect, } from 'react';
import { connect } from "react-redux";
import Loading from './Loading';
import './App.css';
import { Routes, Route } from 'react-router-dom';
import Main from './Main/Main';
import Personalization from './Personalization/Personalization';
import MainAuthorized from './Main/MainAuthorized';
import BirthYearModalWindow from './BirthYearModalWindow';
import { useState } from 'react';
import NotFound from '../../NotFound';
import SearchPage from './Search/SearchPage';
import ConcertPage from '../ConcertPage/ConcertPage';
import OAuthRedirect from '../../OAuthRedirect';

function App({ props, logIn }) {
  const [isLoading, setisLoading] = useState(true);
  const [failed, setFailed] = useState(false);
  useEffect(() => {
    logIn().then(() => {
      setisLoading(false);
    }).catch((error) => {
      typeof error.response === 'undefined' && setFailed(true);
      setisLoading(false);
    });
  }, [logIn]);
  return failed === true ? <h1 style = {{    
    textAlign: "center",
    top: "40vh",
    position: "relative",
    color: "brown"
}}>Server Load Failed</h1> : (
    isLoading === true ? (<Loading />) : (<>
      <div id='invisibleTop' >
        <Header />
      </div>
      <div className='emptyTop'></div>
      <div id="bodyElement">
        <div id="inBody">
          <Routes>
            {props.isLoggedIn ? <Route path="/" element={<MainAuthorized />} /> : <Route path="/" element={<Main />} />}
            {props.isLoggedIn && <Route path="/personalization" element={<Personalization />}></Route>}
            <Route path="/search" element={<SearchPage />} />
            <Route path="/concerts/:concertId" element={<ConcertPage />} />
            <Route path="/OAuth" element = {<OAuthRedirect/>}/>
            <Route path="*" element={<NotFound />} />
          </Routes>
        </div>
        <Footer />
      </div>
      {!localStorage.getItem("cookieModalWindowDisabled") && !props.cookieConfirmationFlag && <CookieModalWindow />}
      {props.isLoggedIn && props.cookieConfirmationFlag && props.user.birthDate === null && !props.birthDateModalDisabled
        && <BirthYearModalWindow />}
    </>)
  );
}

const mapStateToProps = state => {
  return {
    props: {
      isLoggedIn: state.isLoggedIn,
      user: state.user,
      cookieConfirmationFlag: state.cookieConfirmationFlag,
      birthDateModalDisabled: state.birthDateModalDisabled
    }
  }
}

const mapDispatchToProps = dispatch => {
  return {
    logIn: () => dispatch(actionCreators.logInThunkActionCreator())
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(App);