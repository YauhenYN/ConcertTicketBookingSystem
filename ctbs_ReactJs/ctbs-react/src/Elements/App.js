import Footer from './Footer';
import Header from './Header';
import CookieModalWindow from './CookieModalWindow';
import store from "../store";
import React, { useEffect, useState } from 'react';
import * as conf from '../configuration';
import * as thunks from '../thunkActionCreators';


function App() {
  return (
    <><Header />
      <div id="bodyElement">
        <div id="inBody">

        </div>
        <Footer />
      </div>
      {store.getState().isLoggedIn && <CookieModalWindow />}</>
  );
}

export default App;
