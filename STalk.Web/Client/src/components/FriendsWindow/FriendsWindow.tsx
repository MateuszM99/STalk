import React from 'react'
import './style.scss'

function FriendsWindow() {
    return (
        <div className="col-md-10">
            <div>
                <div className="friends__search__friend border-bottom">
                    <h6>Add a friend</h6>
                    <div className="friends__search__friend__input__wrapper">
                    <input></input>
                    <button className="friends__action__button">Search</button>
                    </div>
                </div>
                <div className="friends__add__friend border-bottom">
                        <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                        <p>SomeUser</p>
                        <button className="friends__action__button">Add</button>
                </div>
                <div className="friends__friend__requests border-bottom">
                    <h6>Friend Requests</h6>
                    <div className="friends__friend__requests__user">
                        <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                        <p>SomeUser</p>
                        <span>
                            <button className="friends__action__button">Accept</button>
                            <button className="friends__action__button">Decline</button>
                        </span>
                    </div>
                </div>
                <div className="friends__friends__list border-bottom">
                <h6>Friends</h6>
                <div className="friends__friend__requests__user">
                        <img className="profile-image" src="https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg" alt=""/>
                        <p>SomeUser</p>
                        <span>
                            <button className="friends__action__button">Delete</button>
                        </span>
                </div>
                </div>
            </div>
        </div>
    )
}

export default FriendsWindow