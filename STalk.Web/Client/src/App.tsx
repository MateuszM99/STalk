import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import MainWindow from './components/MainWindow/MainWindow';
import SignIn from './components/SignIn/SignIn';
import SignUp from './components/SignUp/SignUp';
import PasswordForgetWindow from './components/PasswordForgetWindow/PasswordForgetWindow'
import PasswordResetWindow from './components/PasswordResetWindow/PasswordResetWindow'
import './custom.css'

export default () => (
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
        <Route exact path="/reset/:userId/:token">
            <PasswordResetWindow/>
        </Route>
        <Route path='/sTalk'>
            <MainWindow/>
        </Route>
        <Route path="/">
        </Route>
    </div>
);
