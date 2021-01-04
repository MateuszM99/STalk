import React from 'react'
import ChatsList from '../ChatsList/ChatsList'
import ChatWindow from '../ChatWindow/ChatWindow'
import ChatNavbar from '../ChatNavbar/ChatNavbar'
import FriendsWindow from '../FriendsWindow/FriendsWindow'
import ProfileWindow from '../ProfileWindow/ProfileWindow'
import './style.scss';

function MainChat() {
    return (
        <div className="container fill">
            <div className="row fill no-gutters">
                <ChatNavbar></ChatNavbar>
                <ProfileWindow/>
            </div>
        </div>
    )
}

export default MainChat
