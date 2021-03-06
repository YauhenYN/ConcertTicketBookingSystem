import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './Elements/App/App';
import reportWebVitals from './reportWebVitals';
import store from "./store";
import { Provider } from 'react-redux'
import { HashRouter } from 'react-router-dom';
import { Routes, Route } from 'react-router-dom';
import Privacy from './Elements/Privacy/Privacy';
import * as actionCreators from "./actionCreators";
import PaymentSuccess from './PaymentSuccessPage';
import PaymentCancel from './PaymentCancel';


store.dispatch(actionCreators.EmptyStateActionCreator());


if(window.location.hash === "#_=_") window.location.href = window.location.href.replace("#_=_", "");

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <HashRouter>
        <Routes>
          <Route path="*" element={<App />} />
          <Route path="/privacy" element={<Privacy />} />
          <Route path="/Payment/Success" element={<PaymentSuccess />} />
          <Route path="/Payment/Cancel" element={<PaymentCancel />} />
        </Routes>
      </HashRouter>
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
