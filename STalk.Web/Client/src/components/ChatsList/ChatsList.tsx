import React from 'react';
import ChatWindow from '../ChatWindow/ChatWindow';
import './style.scss';
import FriendChat from './FriendChat';
import SearchIcon from '@material-ui/icons/Search';
import MenuIcon from '@material-ui/icons/Menu';
import MessageIcon from '@material-ui/icons/Message';
import CachedIcon from '@material-ui/icons/Cached';
import PeopleIcon from '@material-ui/icons/People';
import * as signalR from "@microsoft/signalr";
import { connect } from 'http2';
import * as worker from '../../registerServiceWorker'
import {getUsersContactsRequest} from '../../services/api/ContactsRequests'

let conversationsCount: number;
let connection: signalR.HubConnection;

//let connection: signalR.HubConnection;

class ChatsList extends React.Component {
    state = {
        conversationsCount: 0,
        conversations: [],
        friends: [],
        searchString: '',
    }
    constructor(props) {
        super(props);
        /*worker.createSignalR().then((resolve) => {
            connection = resolve
            connection.invoke("GetConversationsCount");
            connection.invoke("GetConversations");
            connection.on("UpdateConversationCount", (count) => {
                console.log("pobrałem ilość konwersacji!!")
                console.log(count);
                //conversationsCount = 10;
                this.setState({ conversationsCount: count });
            });
            connection.on("UpdateConversations", (conversations) => {
                console.log("Pobrałem konwersacje");
                console.log(conversations);
                this.setState({ conversations: conversations })
            })
        })*/
    }

    async componentDidMount(){
             
    try{
        let response = await getUsersContactsRequest();
        this.setState({friends: response.data.users})          
        //setUserFindMessage(response.data.message);
        console.log(response.data.users);
    } catch(err) {
        //setUserFindMessage("Invalid input")
    }       
              
    }

    handleSearchChange = (e) => {
        console.log(this.state.searchString);
        this.setState({searchString : e.target.value});
    }
    
    render() {
        return (
            <div className="col-md-2 border-right">
                <div className="conversations-info border-bottom">
                    <PeopleIcon />
                    <span>
                        <h6>Friends</h6>
                        <p>{this.state.conversationsCount} conversations</p>
                    </span>
                </div>
                <div className="search-box">
                    <div className="input-wrapper">
                        <SearchIcon style={{ marginLeft: '10px' }} />
                        <input placeholder="Search here" type="text" onChange={this.handleSearchChange}/>
                    </div>
                </div>
                {this.state.searchString == '' ? this.state.conversations?.map(conversation => 
                    <FriendChat key={conversation.id} conversationId={conversation.id} lastMessage={conversation.lastMessage} recieverName={conversation.recieverName}/>
                ) : null}
                {this.state.searchString != '' ? this.state.friends?.filter(friend => friend.username.includes(this.state.searchString)).map(friend =>
                    <FriendChat key={friend.id} recieverName={friend.username} reciverId={friend.id}/>
                    ) : null}
            </div>
        )
    }
}

export default ChatsList
