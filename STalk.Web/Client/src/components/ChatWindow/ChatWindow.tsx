import React,{useState} from 'react';
import './style.scss';
import SendIcon from '@material-ui/icons/Send';
import Message from './Message';
import ReplyMessage from './ReplyMessage';
import AddIcon from '@material-ui/icons/Add';

function ChatWindow() {

    const [isAddToConvDisplayed,setIsAddToConvDisplayed] = useState(false);

    return (
        <div className="col-md-8 flex">
            <div className="settings-tray">
                <div className="friend-drawer no-gutters friend-drawer--grey">
                    <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                    <div className="text">
                        <h6>Robo Cop</h6>                       
                    </div>
                    <div className="add__to__conversation">
                        <span style={isAddToConvDisplayed ? {display : 'flex'} : {display : 'none'}}>
                            <input type="text" placeholder="Enter username"></input>
                            <button>Add</button>
                        </span>
                        <AddIcon style={{color:'#285d80',cursor:'pointer'}} onClick={() => setIsAddToConvDisplayed(!isAddToConvDisplayed)}/>
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
                            <input type="text" placeholder="Type your message here..."/>
                            <input type="file"/>
                            <SendIcon style={{marginRight:'20px'}} />
                        </div>
                    </div>
                </div>    
        </div>
    )
}

export default ChatWindow
