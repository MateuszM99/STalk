import React,{useState} from 'react';
import './style.scss';
import SendIcon from '@material-ui/icons/Send';
import Message from './Message';
import ReplyMessage from './ReplyMessage';
import AddIcon from '@material-ui/icons/Add';
import * as worker from '../../registerServiceWorker'
import {RouteComponentProps,Link,withRouter} from  "react-router-dom"
import { connect } from 'net';
import { saveFileAndAdd, getConversationName } from '../../services/api/ChatRequests'
let connection: signalR.HubConnection;

type PathParamsType = {
    chatId? : string,
}

type PropsType = RouteComponentProps<PathParamsType> & {
  
}

class ChatWindow extends React.Component<PropsType> {
    state = {
        chatId : null,
        userId: "",
        message: "",
        messages: [],
        newMessage: true,
        fileId: null,
        conversationName: "",
        newConversationUserName: ""
    }

    constructor(props) {
        super(props);
        if (localStorage.getItem('userData') != null) {
            worker.createSignalR().then((resolve) => {
                connection = resolve;

                connection.on("LoadConversation", (messages) => {
                    console.log(messages)
                    this.setState({ messages: messages })
                });
                connection.on("RecievePrivateMessage", (senderId, message, fileId) => {
                    this.setState({ newMessage: true })
                })
                if (this.state.chatId != "show" && this.state.chatId != null) {
                    console.log("CHATID: " + this.state.chatId)
                    connection.invoke("GetConversationMessages", this.state.chatId)
                }
            })
        }
        this.handleChange = this.handleChange.bind(this);
        this.checkForEnterToSend = this.checkForEnterToSend.bind(this);
    }

    componentDidMount() {
        if(this.state.chatId != this.props.match.params.chatId){
            this.setState({chatId : this.props.match.params.chatId });
        }
        this.setState({ userId: JSON.parse(localStorage.getItem('userData')).user.id });
        console.log("userID: " + this.state.userId)
        // tu pobierasz konwersacje z tym id czatu
    }

    componentDidUpdate() {
        if(this.state.chatId != this.props.match.params.chatId){
            this.setState({ chatId: this.props.match.params.chatId }, () => {
                console.log("userID: " + this.state.userId)
                getConversationName(this.state.chatId, this.state.userId).then((response) => {
                    if (response.status == 200) {
                        console.log("ConversationName: " + response.data)
                        this.setState({ conversationName: response.data })
                        connection.invoke("GetConversationMessages", this.state.chatId)
                    }
                })
            });
        }
        if (this.state.newMessage != false) {
            if (this.state.chatId != "show" && this.state.chatId != null) {
                console.log("CHATID: " + this.state.chatId)
                connection.invoke("GetConversationMessages", this.state.chatId)
                this.setState({ newMessage: false })
            }
        }
        // tu pobierasz konwersacje pod warunkiem ze zmienilo sie id chatu
    }

    checkForEnterToSend(event: React.KeyboardEvent) {
        console.log(event.keyCode)
        if (event.keyCode === 13) {
            event.preventDefault();
            //abcde - ID usera
            connection.invoke("SendMessage", this.state.chatId, this.state.message, this.state.fileId)
            this.setState({ message: "" })
            this.setState({ fileId: null })
        }
    }

    addUserToConversation = () => {
        connection.invoke("AddUserToConversation", this.state.chatId, this.state.newConversationUserName);
        this.setState({ newConversationUserName: "" })
    }

    clickedSend() {
        connection.invoke("SendMessage", this.state.chatId, this.state.message, this.state.fileId)
        this.setState({ message: "" })
        this.setState({fileId: null})
    }
    checkForEneterInNewUser(event: React.KeyboardEvent) {
        if (event.keyCode === 13) {
            event.preventDefault()
            this.addUserToConversation()
        }
    }
    handleChange(event) {
        this.setState({message: event.target.value})
    }
    handleNewUserChange = (event) => {
        this.setState({ newConversationUserName: event.target.value })
    }
    sendAndAttachFile(e) {
        let formData = new FormData();
        formData.append('file', e.target.files[0]);
        formData.append('userId', JSON.parse(localStorage.getItem('userData')).user.id)
        console.log(formData)
        saveFileAndAdd(formData).then((response) => {
            console.log(response)
            this.setState({ fileId: response.data })
        })
    };

    render() {

        if(this.state.chatId == "show"){
            return (
                <div></div>
            )
        }

        return (
            <div className="col-md-8 flex">
                <div className="settings-tray">
                    <div className="friend-drawer no-gutters friend-drawer--grey">
                        <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                        <div className="text">
                            <h6>{ this.state.conversationName }</h6>                       
                        </div>
                        <div className="add__to__conv">
                            <span>
                                <input type="text" placeholder="Dodaj nowego użytkownika" onChange={this.handleNewUserChange} onKeyDown={(event) => this.checkForEneterInNewUser(event)}></input>
                                <button className="add__to__conv__button" onClick={this.addUserToConversation}>Add</button>
                            </span>                           
                        </div>
                    </div>
                </div>
                <div className="chat-panel overflow-auto flex">
                    {this.state.messages.map(message => {
                        return message.sendByMe == true ? <Message message={message.message} senderId={message.senderId} fileId={message.fileId} /> :
                            <ReplyMessage message={message.message} senderId={message.senderId} senderName={message.senderName} fileId={message.fileId} />
                        })
                    }
                </div>
                <div className="row mt-auto">
                        <div className="col-12">
                        <div className="chat-box-tray">
                            <input type="text" placeholder="Type your message here..." onKeyDown={(event) => this.checkForEnterToSend(event)} value={this.state.message} onChange={this.handleChange} />
                            <input type="file" onChange={(e) => this.sendAndAttachFile(e) } />
                                <SendIcon style={{ marginRight: '20px' }} onClick={() => this.clickedSend()}/>
                            </div>
                        </div>
                    </div>    
            </div>
        )
    }
}

export default withRouter(ChatWindow);
