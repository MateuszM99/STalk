import React from 'react'
import {addToContactsRequest} from '../../services/api/ContactsRequests'

interface Props{
    username: string;
    image: File
}

export const AddFriend: React.FC<Props> = ({username,image}) => {

    const sendFriendsRequest = async (username) => {
        console.log(username);

        try{
            let response = await addToContactsRequest(username); 
        } catch(err) {
            
        }
    }

    return (
        <div>
            <img className="profile-image"  alt=""/>
            <p>{username}</p>
            <button className="friends__action__button" onClick={() => sendFriendsRequest(username)}>Add</button>
        </div>
    )
}

export default AddFriend

