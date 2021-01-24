import React from 'react'
import {acceptAddToContactsRequest,declineAddToContactsRequest} from '../../services/api/ContactsRequests'

interface Props{
    id : number;
    username: string;
    image: File;
    onDecline : (id) => void;
    onAccept : (id) => void;
}

export const FriendRequest: React.FC<Props> = ({id,username,image,onAccept,onDecline}) => {

    return (
        <div className="friends__friend__requests__user">
            <img className="profile-image" src={`data:image/jpeg;base64,${image}`} alt=""/>
            <p>{username}</p>
            <span>
                <button className="friends__action__button" onClick={() => onAccept(id)}>Accept</button>
                <button className="friends__action__button" onClick={() => onDecline(id)}>Decline</button>
            </span>
        </div>
    )
}

export default FriendRequest
