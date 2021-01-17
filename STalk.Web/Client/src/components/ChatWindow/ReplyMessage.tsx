import React from 'react'
import {Link} from 'react-router-dom'

function ReplyMessage() {
    return (
        <div className="row no-gutters">
            <div className="col-md-3 offset-md-9">
                <div className="chat-bubble chat-bubble--right">
                    Hello dude!
                    <Link to={''} style={{color : 'lightgrey',textDecoration: 'underline'}}>File download link</Link>
                </div>
            </div>
        </div>
    )
}

export default ReplyMessage
