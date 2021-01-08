import React from 'react'

interface Props{
    username: string;
    image: File
}

export const  Friend: React.FC<Props> = ({username,image}) => {
    return (
        <div className="friends__friend__requests__user">
            <img className="profile-image" alt=""/>
            <p>{username}</p>
            <span>
                <button className="friends__action__button">Delete</button>
            </span>
        </div>
    )
}
export default Friend
