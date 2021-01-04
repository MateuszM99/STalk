import React,{ useEffect, useState } from 'react'
import './style.scss'

function ProfileWindow() {
    const [image,setImage] = useState<any>("https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg");
    
    const onSelectFile = (event) => {
        if (event.target.files && event.target.files[0]) {
          var reader = new FileReader();
        
          reader.readAsDataURL(event.target.files[0]);

          reader.onload = (event) => { 
              if(event.target != null){
                setImage(event.target.result)
              }
          }

        }
    }

    return (
        <div className="profile__main col-md-10">
            <div className="profile__image__change">
                <img className="profile-image" src={image} alt=""/>
                <input type="file" accept="image/*" onChange={onSelectFile}></input>
                <button className="profile__switch__button">Save profile image</button>
            </div>
            <div className="profile__form__control">
            <button className="profile__switch__button">Change password</button>
            <button className="profile__switch__button">Change email</button>
            <button className="profile__switch__button">Change username</button>
            </div>
        </div>
    )
}

export default ProfileWindow
