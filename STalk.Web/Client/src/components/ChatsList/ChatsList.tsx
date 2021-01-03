import React from 'react';
import ChatWindow from '../ChatWindow/ChatWindow';
import './style.scss';
import FriendChat from './FriendChat';
import SearchIcon from '@material-ui/icons/Search';
import MenuIcon from '@material-ui/icons/Menu';
import MessageIcon from '@material-ui/icons/Message';
import CachedIcon from '@material-ui/icons/Cached';

function ChatsList() {
    return (
        <div className="col-md-2 border-right">
            <div className="settings-tray">
            <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt="Profile img"/>
                <span className="settings-tray--right">
                    <CachedIcon/>
                    <MessageIcon/>
                    <MenuIcon/>
                </span>
            </div>
            <div className="search-box">
                <div className="input-wrapper">                   
                    <SearchIcon/>
                    <input placeholder="Search here" type="text"/>
                </div>
            </div>
            <FriendChat/>
        </div>
    )
}

export default ChatsList
