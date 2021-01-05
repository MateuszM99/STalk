import React from 'react'
import ChatsList from '../ChatsList/ChatsList'
import ChatWindow from '../ChatWindow/ChatWindow'
import ChatNavbar from '../ChatNavbar/ChatNavbar'
import FriendsWindow from '../FriendsWindow/FriendsWindow'
import ProfileWindow from '../ProfileWindow/ProfileWindow'
import { Route } from 'react-router-dom';
import './style.scss';

function MainWindow() {
    return (
        <div className="container fill">
            <div className="row fill no-gutters">
                <ChatNavbar></ChatNavbar>
                <Route exact path={"/sTalk/chat"}>
                    <ChatsList/>
                    <ChatWindow/>
                </Route>
                <Route exact path={"/sTalk/friends"}>
                    <FriendsWindow/>
                </Route>
                <Route exact path={"/sTalk/profile"}>
                    <ProfileWindow/>
                </Route>        
            </div>
        </div>
    )
}

export default MainWindow
