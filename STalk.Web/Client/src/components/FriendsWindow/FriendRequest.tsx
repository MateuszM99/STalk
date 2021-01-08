import React from 'react'

interface Props{
    username: string;
    image: File
}

export const FriendRequest: React.FC<Props> = ({username,image}) => {
    return (
        <div className="friends__friend__requests__user">
            <img className="profile-image" alt=""/>
            <p>{username}</p>
            <span>
                <button className="friends__action__button">Accept</button>
                <button className="friends__action__button">Decline</button>
            </span>
        </div>
    )
}

export default FriendRequest
