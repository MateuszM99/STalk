import React from 'react'

function PasswordChangeForm() {
    return (
        <div className="profile__edit__form">
                <span>
                    <label>Enter old password</label>
                    <input type="text"></input>
                </span>
                <span>
                    <label>Enter new password</label>
                    <input type="text"></input>
                </span>
                <span>
                    <label>Confirm new password</label>
                    <input type="text"></input>
                </span>
        </div>
    )
}

export default PasswordChangeForm
