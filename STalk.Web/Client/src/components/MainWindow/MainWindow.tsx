import React from 'react'
import ChatsList from '../ChatsList/ChatsList'
import ChatWindow from '../ChatWindow/ChatWindow'
import ChatNavbar from '../ChatNavbar/ChatNavbar'
import FriendsWindow from '../FriendsWindow/FriendsWindow'
import ProfileWindow from '../ProfileWindow/ProfileWindow'
import { Route } from 'react-router-dom';
import './style.scss';
import * as signalR from "@microsoft/signalr";
import * as worker from '../../registerServiceWorker'
let connection: signalR.HubConnection;
let conversationsCount: number;
class MainWindow extends React.Component {

    constructor(props) {
        super(props);
        if (localStorage.getItem('userData') != null) {
            worker.createSignalR().then((resolve) => {
                connection = resolve
            });
        }
    }

    render() {
        return (
            <div className="container fill">
                <div className="row fill no-gutters">
                    <ChatNavbar />
                    <Route path={"/sTalk/chat/:chatId"}>
                        <ChatsList {...conversationsCount}/>
                        <ChatWindow/>
                    </Route>
                    <Route path="/sTalk/friends">
                        <FriendsWindow/>
                    </Route>
                    <Route path="/sTalk/profile">
                        <ProfileWindow/>
                    </Route>        
                </div>
            </div>
        )
    }
}

export default MainWindow
