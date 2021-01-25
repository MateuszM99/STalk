import React from 'react'
import { Link } from 'react-router-dom'

const baseUrl = 'https://localhost:44338/api';

interface Props {
    message: string;
    senderId: string;
    fileId?: number;
}


export const Message: React.FC<Props> = ({ message, senderId, fileId }) => {
    return (
        <div className="row no-gutters">
            <div className="col-md-3">
                <div className="chat-bubble chat-bubble--left">
                    {message}
                    <div style={fileId == null ? { display: "none" } : { display: "block" }}>
                        <a target="_blank" href={`https://localhost:44338/api/chat/getFile?fileId=${fileId}`} style={{ textDecoration: 'underline' }} download>Download attachement</a>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Message
