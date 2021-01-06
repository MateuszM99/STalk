import React from 'react'
import './style.scss'
import PeopleIcon from '@material-ui/icons/People';
import PersonIcon from '@material-ui/icons/Person';
import ChatBubbleIcon from '@material-ui/icons/ChatBubble';
import PowerSettingsNewIcon from '@material-ui/icons/PowerSettingsNew';
import { Link } from 'react-router-dom';

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
                <Link to={`/sTalk/chat`}>
                    <div>
                        <ChatBubbleIcon/>
                        <p>Conversations</p>
                    </div>
                </Link>
                <Link to={`/sTalk/friends`}>
                <div>
                    <PeopleIcon/>
                    <p>Friends</p>
                </div>
                </Link>
                <Link to={`/sTalk/profile`}>
                <div>
                    <PersonIcon/>
                    <p>Profile</p>
                </div>
                </Link>
                <div className="offset">
                    <PowerSettingsNewIcon/>
                    <p>Sign out</p>
                </div>
            </div>
        </div>
    )
}

export default ChatNavbar
