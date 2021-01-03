import React from 'react'
import ChatsList from '../ChatsList/ChatsList'
import ChatWindow from '../ChatWindow/ChatWindow'
import './style.scss';

function MainChat() {
    return (
        <div className="container fill">
            <div className="row fill no-gutters">
                <ChatsList></ChatsList>
                <ChatWindow></ChatWindow>
            </div>
        </div>
    )
}

export default MainChat
