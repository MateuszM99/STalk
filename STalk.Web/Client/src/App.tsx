import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import MainChat from './components/MainChat/MainChat';
import SignIn from './components/SignIn/SignIn';
import SignUp from './components/SignUp/SignUp';

import './custom.css'

export default () => (
    <div style={{height:"100vh"}}>
        <Route exact path="/" component={SignIn}/>
        <Route exact path="/signUp" component={SignUp}/>
        <Route exact path='/chat' component={MainChat} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
    </div>
);
