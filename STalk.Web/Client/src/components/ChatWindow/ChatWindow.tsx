import React,{useState} from 'react';
import './style.scss';
import SendIcon from '@material-ui/icons/Send';
import Message from './Message';
import ReplyMessage from './ReplyMessage';
import AddIcon from '@material-ui/icons/Add';
import * as worker from '../../registerServiceWorker'
import {RouteComponentProps,Link,withRouter} from  "react-router-dom"
import { connect } from 'net';
import { saveFileAndAdd } from '../../services/api/ChatRequests'
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
        fileId: null
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

    componentDidMount(){
        //if(this.state.chatId != this.props.match.params.chatId){
        //    this.setState({chatId : this.props.match.params.chatId });
        //}
        // tu pobierasz konwersacje z tym id czatu
    }

    componentDidUpdate() {
        if(this.state.chatId != this.props.match.params.chatId){
            this.setState({ chatId: this.props.match.params.chatId });
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
    clickedSend() {
        connection.invoke("SendMessage", this.state.chatId, this.state.message, this.state.fileId)
        this.setState({ message: "" })
        this.setState({fileId: null})
    }
    handleChange(event) {
        this.setState({message: event.target.value})
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
                            <h6>Robo Cop</h6>                       
                        </div>
                    </div>
                </div>
                <div className="chat-panel overflow-auto flex">
                    {this.state.messages.map(message => {
                        return message.sendByMe == true ? <Message message={message.message} senderId={message.senderId} fileId={message.fileId} /> : <ReplyMessage message={message.message} senderId={message.senderId} fileId={message.fileId}/>
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
