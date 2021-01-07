import React from 'react'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import {usernameChangeRequest} from '../../services/api/ProfileRequests'

function UsernameChangeForm() {
    return (
        <div className="profile__edit__form">
            <Formik 
            initialValues={{
                username : '',
                confirmUsername : ''
            }}

            validationSchema = {Yup.object({
                username : Yup.string()
                        .required('Username is required'),
                confirmUsername : Yup.string()
                        .required('Username confirm is required')
                        .oneOf([Yup.ref('username'),null],'Usernames must match')
            })}

            onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) =>  {   
                if(values.username && values.confirmUsername){
                    try{
                        let response = await usernameChangeRequest(values);
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
            {({ errors,touched,isSubmitting,status}) => (
                <Form>
                    <span>
                        <label>Enter new username</label>
                        <Field type="text" name="username" placeholder="Enter new username"></Field>
                        {errors.username && touched.username ? <div className="validation">{errors.username}</div> : null}
                    </span>
                    <span>
                        <label>Confirm new username</label>
                        <Field type="text" name="confirmUsername" placeholder="Enter new username again"></Field>
                        {errors.confirmUsername && touched.confirmUsername ? <div className="validation">{errors.confirmUsername}</div> : null}
                    </span>
                    <button className="profile__switch__button">
                        {isSubmitting ? 'Saving ...' : 'Save changes'}
                    </button>
                    {status && status.errorMessage ? (
                                <div className="validation">{status.errorMessage}</div>
                            ) : null}
                </Form>
            )}
            </Formik>
        </div>
    )
}

export default UsernameChangeForm
