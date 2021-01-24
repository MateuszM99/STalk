import * as React from 'react';
import { Route,Redirect } from 'react-router';
import PrivateRoute from './PrivateRoute';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import MainWindow from './components/MainWindow/MainWindow';
import SignIn from './components/SignIn/SignIn';
import SignUp from './components/SignUp/SignUp';
import PasswordForgetWindow from './components/PasswordForgetWindow/PasswordForgetWindow';
import PasswordResetWindow from './components/PasswordResetWindow/PasswordResetWindow';
import './custom.css'

export default() => (
    <div style={{height:"100vh"}}>
        <Route exact path="/signUp" >
            <SignUp/>
        </Route>
        <Route exact path="/signIn" >
            <SignIn/>
        </Route>
        <Route exact path="/forgot">
            <PasswordForgetWindow/>
        </Route>
        <Route exact path="/reset">
            <PasswordResetWindow/>
        </Route>
        <PrivateRoute path='/sTalk' component={MainWindow}/>
        <Route exact path="/">
            <Redirect to="/sTalk/chat/show"/>
        </Route>
    </div>
);
