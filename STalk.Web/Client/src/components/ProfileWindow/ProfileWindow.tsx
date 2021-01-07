import React,{ ChangeEvent, useEffect, useState } from 'react'
import PasswordChangeForm from './PasswordChangeForm'
import UsernameChangeForm from './UsernameChangeForm'
import EmailChangeForm from './EmailChangeForm'
import './style.scss'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import {profileImageChangeRequest} from '../../services/api/ProfileRequests'

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

    let ProfileForm;
    let text;
    if (renderState == 1) {
        ProfileForm = PasswordChangeForm;
        text = "Change password";
    } else if (renderState == 2) {
        ProfileForm = EmailChangeForm;
        text = "Change email";
    } else if (renderState == 3) {
        ProfileForm = UsernameChangeForm;
        text = "Change username";
    }
    

    return (
        <div className="profile__main col-md-10">
            <Formik
            initialValues={{
                image : null
            }}

            validationSchema = {Yup.object({
                image : Yup.mixed()
                .required('Image is required'),  
            })}

            onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) =>  {   
                if(values.image){
                    try{
                        let response = await profileImageChangeRequest(values);
                        setSubmitting(false);
                        resetForm();
                    } catch(err){
                        setSubmitting(false);
                        resetForm();
                        setStatus({
                            errorMessage : err.message
                        });
                    }      
                }
            }}
            >
            {({ errors,touched,isSubmitting,status,setFieldValue}) => (
                <Form>
                    <div className="profile__image__change">
                        <img className="profile-image" src={image} alt=""/>
                        <input type="file" accept="image/*" id="image" name = "image" onChange={(event) => {
                            setFieldValue("image", event.currentTarget.files[0])
                            onSelectFile(event);
                        }}></input>
                        <button className="profile__switch__button" type="submit">
                            {isSubmitting ? 'Saving ...' : 'Save profile image'}
                        </button>
                        {errors.image && touched.image ? <div className="cm__products__create__container__validation">{errors.image}</div> : null}
                        {status && status.errorMessage ? (<div className="validation">{status.errorMessage}</div>) : null}
                    </div>
                </Form>
            )}
            </Formik>
            <div className="profile__form__control">
            <button className="profile__switch__button" onClick={() => setRenderState(1)}>Change password</button>
            <button className="profile__switch__button" onClick={() => setRenderState(2)}>Change email</button>
            <button className="profile__switch__button" onClick={() => setRenderState(3)}>Change username</button>
            </div>
            <h5>{text}</h5>
            <ProfileForm/>
        </div>
    )
}

export default ProfileWindow
