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
                connection.invoke("GetMyMessages");
                connection.on("UpdateAll", function (messages) {
                    console.log("Pobrałem wszystkie wiadomości usera! (odświeżył stronę)");
                    console.log(messages);
                    localStorage.setItem("messages", messages);
                });
                connection.on("RecievePrivateMessage", (senderId, message) => {
                    console.log("Odebrano wiadomość od: " + senderId)
                    console.log("Wiadomość: " + message)
                });
            })

            //connection = worker.createSignalR();

            //connection.invoke("SendMessage", friendId, message);
            console.log(connection)
            //connection.on("SendPrivateMessage")
            //connection.on("recievedPrivate", (fromUser, message) => {
            //    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            //    var encodedMsg = fromUser + " says " + msg;
            //});
        }
    }

    render() {
        return (
            <div className="container fill">
                <div className="row fill no-gutters">
                    <ChatNavbar />
                    <Route exact path={"/sTalk/chat/:chatId"}>
                        <ChatsList {...conversationsCount}/>
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
}

export default MainWindow
