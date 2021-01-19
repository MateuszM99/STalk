import React,{useState} from 'react';
import './style.scss';
import SendIcon from '@material-ui/icons/Send';
import Message from './Message';
import ReplyMessage from './ReplyMessage';
import AddIcon from '@material-ui/icons/Add';
import * as worker from '../../registerServiceWorker'

let connection: signalR.HubConnection;

class ChatWindow extends React.Component {
    state = {
        userId: "",
        message: ""
    }

    constructor(props) {
        super(props);
        if (localStorage.getItem('userData') != null) {
            worker.createSignalR().then((resolve) => {
                connection = resolve
            });
        }
        this.handleChange = this.handleChange.bind(this);
        this.checkForEnterToSend = this.checkForEnterToSend.bind(this);
    }

    checkForEnterToSend(event: React.KeyboardEvent) {
        console.log(event.keyCode)
        if (event.keyCode === 13) {
            event.preventDefault();
            //abcde - ID usera
            connection.invoke("SendMessage", "abcde", this.state.message)
        }
    }

    handleChange(event) {
        this.setState({message: event.target.value})
    }

    render() {
        return (
            <div className="col-md-8 flex">
                <div className="settings-tray">
                    <div className="friend-drawer no-gutters friend-drawer--grey">
                        <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                        <div className="text">
                            <h6>Robo Cop</h6>                       
                        </div>
                    </div>
                </div>
                <div className="chat-panel overflow-auto flex">
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                    <Message/>
                    <ReplyMessage/>
                </div>
                <div className="row mt-auto">
                        <div className="col-12">
                        <div className="chat-box-tray">
                            <input type="text" placeholder="Type your message here..." onKeyDown={(event) => this.checkForEnterToSend(event)} value={this.state.message} onChange={this.handleChange} />
                                <input type="file"/>
                                <SendIcon style={{ marginRight: '20px' }}/>
                            </div>
                        </div>
                    </div>    
            </div>
        )
    }
}

export default ChatWindow
