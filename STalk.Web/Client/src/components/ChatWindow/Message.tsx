import React from 'react'
import { Link } from 'react-router-dom'

function Message() {
    return (
        <div className="row no-gutters">
            <div className="col-md-3">
                <div className="chat-bubble chat-bubble--left">
                    Hello dude!
                    <div>
                    <Link to={''} style={{textDecoration: 'underline'}}>File download link</Link>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Message
