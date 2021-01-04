import React from 'react';
import ChatWindow from '../ChatWindow/ChatWindow';
import './style.scss';
import FriendChat from './FriendChat';
import SearchIcon from '@material-ui/icons/Search';
import MenuIcon from '@material-ui/icons/Menu';
import MessageIcon from '@material-ui/icons/Message';
import CachedIcon from '@material-ui/icons/Cached';
import PeopleIcon from '@material-ui/icons/People';

function ChatsList() {
    return (
        <div className="col-md-2 border-right">
            <div className="conversations-info border-bottom">
                <PeopleIcon/>
                <span>
                <h6>Friends</h6>
                <p>206 conversations</p>
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
