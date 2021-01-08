import React from 'react'

interface Props{
    username: string;
    image: File
}

export const AddFriend: React.FC<Props> = ({username,image}) => {
    return (
        <div>
            <img className="profile-image"  alt=""/>
            <p>{username}</p>
            <button className="friends__action__button">Add</button>
        </div>
    )
}

export default AddFriend

