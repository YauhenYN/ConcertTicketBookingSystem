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

function App({ props, logIn }) {
  const [isLoading, setisLoading] = useState(true);
  useEffect(() => {
    logIn().then(() =>{
      setisLoading(false);
    });
  }, [logIn]);
   return (
    isLoading === true ? (<Loading />) : (<>
    <div id='invisibleTop'/>
    <Header />
      <div id="bodyElement">
        <div id="inBody">
          <Routes>
            {props.isLoggedIn ? <Route path="/" element={<MainAuthorized/>}/> : <Route path="/" element={<Main/>}/>}
            {props.isLoggedIn && <Route path="/personalization" element={<Personalization/>}></Route>}
            <Route path = "*" element = {<NotFound/>}/>
          </Routes>
        </div>
        <Footer />
      </div>
      {props.isLoggedIn && !props.user.cookieConfirmationFlag && <CookieModalWindow />}
      {props.isLoggedIn && props.user.cookieConfirmationFlag && props.user.birthDate === null && props.birthDateModalDisabled !== true 
      && <BirthYearModalWindow />}
      </>)
  );
}

const mapStateToProps = state => {
  return {
    props: {
      isLoggedIn: state.isLoggedIn,
      user: state.user,
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