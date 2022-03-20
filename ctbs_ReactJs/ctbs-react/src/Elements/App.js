import Footer from './Footer';
import Header from './Header';
import CookieModalWindow from './CookieModalWindow';
import store from "../store";
import * as actionCreators from "../actionCreators";
import React, { useEffect, } from 'react';
import { connect } from "react-redux";
import Loading from './Loading';

store.dispatch(actionCreators.EmptyState());

function App({ props, logIn }) {
  useEffect(() => {
    logIn();
  }, [logIn]);
  return (
    props.isLoading !== 0 ? (
      <Loading/>
    ) : (<><Header />
      <div id="bodyElement">
        <div id="inBody">

        </div>
        <Footer />
      </div>
      {store.getState().isLoggedIn && !store.getState().user.cookieConfirmationFlag && <CookieModalWindow />}</>)
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
