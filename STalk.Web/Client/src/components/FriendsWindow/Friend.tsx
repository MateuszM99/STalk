import React from 'react'

interface Props{
    username: string;
    image: File
    onDelete : (username) => void
}

export const  Friend: React.FC<Props> = ({username,image,onDelete}) => {
    return (
        <div className="friends__friend__requests__user">
            <img className="profile-image" src={`data:image/jpeg;base64,${image}`} alt=""/>
            <p>{username}</p>
            <span>
                <button className="friends__action__button" onClick={() => onDelete(username)}>Delete</button>
            </span>
        </div>
    )
}
export default Friend
