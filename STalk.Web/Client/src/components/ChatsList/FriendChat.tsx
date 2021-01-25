import React from 'react'

import './style.scss';
import {getConversationRequest} from '../../services/api/ChatRequests';
import { useHistory } from 'react-router';

interface Props{
    conversationId? : number;
    lastMessage?: string;
    recieverName: string;
    reciverId?: string;
    newMessage?: boolean;
    onOpen: (convId) => void;
}

export const FriendChat: React.FC<Props> = ({ conversationId, lastMessage, recieverName, reciverId, newMessage, onOpen }) => {
    const history = useHistory();

    const onFriendDrawerClick = async (conversationId, reciverId) => {
        console.log('convIt: ' + conversationId)
        console.log('recieverId: ' + reciverId)
        if (conversationId == null) {
            try{
                let response = await getConversationRequest(reciverId);
                console.log(response.data.id);
                history.push(`/sTalk/chat/${response.data.id}`)
                onOpen(response.data.id)
            }
            catch(err){

            }
        } 
        else {
            onOpen(conversationId)
            history.push(`/sTalk/chat/${conversationId}`)
        }
    }
    return (
        <div className="friend-drawer friend-drawer--onhover" onClick={() => onFriendDrawerClick(conversationId,reciverId)}>
            <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
            <div className="text">
                <h6 style={newMessage == true ? { fontWeight: "bolder" } : { fontWeight: "normal" }}>{recieverName}</h6>
                <p className="text-muted" style={newMessage == true ? { fontWeight: "bolder" } : { fontWeight: "normal" }}>{lastMessage}</p>
            </div>
            <span className="time text-muted small">{}</span>
        </div>
    )
}

export default FriendChat
