import React,{ ChangeEvent, useEffect, useState } from 'react'
import PasswordChangeForm from './PasswordChangeForm'
import UsernameChangeForm from './UsernameChangeForm'
import EmailChangeForm from './EmailChangeForm'
import './style.scss'

function ProfileWindow() {
    const [image,setImage] = useState<any>("https://thumbs.dreamstime.com/b/default-avatar-profile-image-vector-social-media-user-icon-potrait-182347582.jpg");
    const [renderState,setRenderState] = useState(1);


    const onSelectFile = (event: ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
    
            reader.readAsDataURL(file);
    
            reader.onload = () => {
                setImage(reader.result as string);
            }
        }
    }

    const renderComponent = () => {
        return <PasswordChangeForm/>;
    }

    let Form;
    let text;
    if (renderState == 1) {
    Form = PasswordChangeForm;
    text = "Change password";
    } else if (renderState == 2) {
    Form = EmailChangeForm;
    text = "Change email";
    } else if (renderState == 3) {
        Form = UsernameChangeForm;
        text = "Change username";
    }
    

    return (
        <div className="profile__main col-md-10">
            <div className="profile__image__change">
                <img className="profile-image" src={image} alt=""/>
                <input type="file" accept="image/*" onChange={onSelectFile}></input>
                <button className="profile__switch__button">Save profile image</button>
            </div>
            <div className="profile__form__control">
            <button className="profile__switch__button" onClick={() => setRenderState(1)}>Change password</button>
            <button className="profile__switch__button" onClick={() => setRenderState(2)}>Change email</button>
            <button className="profile__switch__button" onClick={() => setRenderState(3)}>Change username</button>
            </div>
            <h5>{text}</h5>
            <Form/>
        </div>
    )
}

export default ProfileWindow
