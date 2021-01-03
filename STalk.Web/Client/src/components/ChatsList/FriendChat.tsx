import React from 'react'
import './style.scss';

function FriendChat() {
    return (
        <div className="friend-drawer friend-drawer--onhover">
                <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                <div className="text">
                    <h6>Robo Cop</h6>
                    <p className="text-muted">Hey!</p>
                </div>
                <span className="time text-muted small">13:21</span>
        </div>
    )
}

export default FriendChat
