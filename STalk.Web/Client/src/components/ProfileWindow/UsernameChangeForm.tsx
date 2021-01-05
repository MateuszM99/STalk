import React from 'react'

function UsernameChangeForm() {
    return (
        <div className="profile__edit__form">
                <span>
                    <label>Enter new username</label>
                    <input type="text"></input>
                </span>
                <span>
                    <label>Confirm new username</label>
                    <input type="text"></input>
                </span>
        </div>
    )
}

export default UsernameChangeForm
