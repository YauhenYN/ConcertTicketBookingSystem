import Footer from './Footer';
import Header from './Header';
import CookieModalWindow from './CookieModalWindow';
import store from "../../store";
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

store.dispatch(actionCreators.EmptyState());

function App({ props, logIn }) {
  useEffect(() => {
    logIn();
  }, [logIn]);
   return (
    props.isLoading !== 0 ? (
      <Loading />
    ) : (<><div id='invisibleTop'/>
    <Header />
      <div id="bodyElement">
        <div id="inBody">
          <Routes>
            {store.getState().isLoggedIn ? <Route path="/" element={<MainAuthorized/>}/> : <Route path="/" element={<Main/>}/>}
            {store.getState().isLoggedIn && <Route path="/personalization" element={<Personalization/>}></Route>}
          </Routes>
        </div>
        <Footer />
      </div>
      {store.getState().isLoggedIn && !store.getState().user.cookieConfirmationFlag && <CookieModalWindow />}
      {store.getState().isLoggedIn && store.getState().user.cookieConfirmationFlag && store.getState().user.birthDate === null && store.getState().birthDateModalDisabled !== true && <BirthYearModalWindow />}
      </>)
  );
}

const mapStateToProps = state => {
  return {
    props: {
      isLoading: state.isLoading
    }
  }
}

const mapDispatchToProps = dispatch => {
  return {
    logIn: () => dispatch(actionCreators.logInThunkAction())
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(App);
