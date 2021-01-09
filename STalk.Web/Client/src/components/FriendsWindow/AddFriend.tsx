import React from 'react'
import {addToContactsRequest} from '../../services/api/ContactsRequests'

interface Props{
    username: string;
    status : number;
    image: File
}

export const AddFriend: React.FC<Props> = ({username,status,image}) => {

    const sendFriendsRequest = async (username) => {
        console.log(username);

        try{
            let response = await addToContactsRequest(username); 
        } catch(err) {
            
        }
    }

    let buttonText = "Add";
    if(status == 0){

    }
    else if(status == 1){
        buttonText = "Invite sent"
    }
    else if(status == 2){
        buttonText = "Friend"
    }

    return (
        <div>
            <img className="profile-image" src={`data:image/jpeg;base64,${image}`} alt=""/>
            <p>{username}</p>
            <button className="friends__action__button" onClick={() => sendFriendsRequest(username)}>{buttonText}</button>
        </div>
    )
}

export default AddFriend

