import React from 'react'
import {acceptAddToContactsRequest,declineAddToContactsRequest} from '../../services/api/ContactsRequests'

interface Props{
    id : number;
    username: string;
    image: File
}

export const FriendRequest: React.FC<Props> = ({id,username,image}) => {

    const acceptFriendsRequest = async (requestId) => {
        try{
            console.log('Request id: ' + requestId);
            let response = await acceptAddToContactsRequest(requestId); 
        } catch(err) {
            
        }
    }

    const declineFriendsRequest = async (requestId) => {
        try{
            let response = await declineAddToContactsRequest(requestId); 
        } catch(err) {
            
        }
    }

    return (
        <div className="friends__friend__requests__user">
            <img className="profile-image" src={`data:image/jpeg;base64,${image}`} alt=""/>
            <p>{username}</p>
            <span>
                <button className="friends__action__button" onClick={() => acceptFriendsRequest(id)}>Accept</button>
                <button className="friends__action__button" onClick={() => declineAddToContactsRequest(id)}>Decline</button>
            </span>
        </div>
    )
}

export default FriendRequest
