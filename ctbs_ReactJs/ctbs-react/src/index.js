import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './Elements/App/App';
import reportWebVitals from './reportWebVitals';
import store from "./store";
import { Provider } from 'react-redux'
import { BrowserRouter } from 'react-router-dom';
import { Routes, Route } from 'react-router-dom';
import Privacy from './Elements/Privacy/Privacy';


ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          <Route path="*" element={<App />} />
          <Route path="/privacy" element = {<Privacy />}></Route>
        </Routes>
      </BrowserRouter>
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
