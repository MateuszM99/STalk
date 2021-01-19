import React, { useEffect, useState} from 'react';
import { Route, Redirect } from 'react-router-dom'
import jwt_decode from "jwt-decode";

interface MyToken {
    name: string;
    exp: number;
  }

const PrivateRoute = ({ component: Component, ...rest }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(null);
  useEffect(() => {
    let user = JSON.parse(localStorage.getItem('userData'));
        if(user != null){
            let token = user.token;
            let tokenExpiration = jwt_decode<MyToken>(token).exp;
            let dateNow = new Date();

            if(tokenExpiration < dateNow.getTime()/1000){
              setIsAuthenticated(false)
            } else {
              setIsAuthenticated(true);
            }
        } else {
          setIsAuthenticated(false)
        }
  },[isAuthenticated])

  if(isAuthenticated == null){
      return (
          <Route {...rest} render={props =>
                  <Redirect to='/signIn' />
          }/>
            )
        }
    return (
    <Route {...rest} render={props =>
      isAuthenticated  ? (
        <Component {...props} />
      ) : (
        <Redirect to='/signIn'/>
      )
    }
    />
  );
};

export default PrivateRoute;