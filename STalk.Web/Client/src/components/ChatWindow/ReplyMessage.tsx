import React from 'react'
import { Link } from 'react-router-dom'

const baseUrl = 'https://localhost:44338/api';


interface Props {
    message: string;
    senderId: string;
    fileId?: number;
}


export const ReplyMessage: React.FC<Props> = ({ message, senderId, fileId }) => {
    return (
        <div className="row no-gutters">
            <div className="col-md-3 offset-md-9">
                <div className="chat-bubble chat-bubble--right">
                    {message}
                    <div style={fileId == null ? { display: "none" } : { display: "block" }}>
                        <a target="_blank" href={`https://localhost:44338/api/chat/getFile?fileId=${fileId}`} style={{ color: 'lightgrey', textDecoration: 'underline' }} download>File download link</a>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ReplyMessage
