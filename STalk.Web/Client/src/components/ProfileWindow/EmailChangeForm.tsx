import React from 'react'

function EmailChangeForm() {
    return (
        <div className="profile__edit__form">
                <span>
                    <label>Enter new email address</label>
                    <input type="text"></input>
                </span>
                <span>
                    <label>Confirm new email address</label>
                    <input type="text"></input>
                </span>
        </div>
    )
}

export default EmailChangeForm
