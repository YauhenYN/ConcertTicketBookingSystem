import * as conf from './configuration';
import * as actionTypes from "./actionTypes"

export const RefreshCodeThunkAction = (access, refresh) => {
    return async function fetchTokenThunk(dispatch){
        fetch(conf.apiLink + conf.refreshAddition + '?refreshToken=' + refresh, {
            method: 'POST',
            headers: { 'Authorization': 'Bearer ' + access }
        })
      .then(res => res.json()).then((result) => {
          dispatch({
            type: actionTypes.RefreshCode,
            accessToken: result.accessToken,
            accessTokenExpires: result.expirationTime,
            refreshToken: result.refreshToken,
            refreshTokenExpires: result.refreshExpirationTime
          })
      })
    }
};