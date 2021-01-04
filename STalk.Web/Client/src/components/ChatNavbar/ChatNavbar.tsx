import React from 'react'
import './style.scss'
import PeopleIcon from '@material-ui/icons/People';
import PersonIcon from '@material-ui/icons/Person';
import ChatBubbleIcon from '@material-ui/icons/ChatBubble';

function ChatNavbar() {
    return (
        <div className="col-md-2 border-right" style={{background: '#285d80'}}>
            <div className="nav__image__display">               
                    <img src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                    <div className="nav__image__display__text">
                        <h6>Robo Cop</h6>    
                        <p>somerandommail@gmail.com</p>                   
                    </div>
            </div>
            <div className="nav__tabs__display">
                <div>
                    <ChatBubbleIcon/>
                    <p>Conversations</p>
                </div>
                <div>
                    <PeopleIcon/>
                    <p>Friends</p>
                </div>
                <div>
                    <PersonIcon/>
                    <p>Profile</p>
                </div>
            </div>
        </div>
    )
}

export default ChatNavbar
