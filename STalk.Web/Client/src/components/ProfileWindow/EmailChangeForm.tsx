import React from 'react'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import {emailChangeRequest} from '../../services/api/ProfileRequests'

function EmailChangeForm() {
    return (
        <div className="profile__edit__form">
            <Formik 
            initialValues={{
                email : '',
                confirmEmail : ''
            }}

            validationSchema = {Yup.object({
                email : Yup.string()
                    .required('Email is required')
                    .email('Email is not valid'),
                confirmEmail : Yup.string().required('Email confirm is required')
                .oneOf([Yup.ref("email"), null], "Emails must match")
            })}

            onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) =>  {   
                if(values.email && values.confirmEmail){
                    try{
                        let response = await emailChangeRequest(values);
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
                        <label>Enter new email address</label>
                        <Field type="text" name="email" placeholder="Enter email"></Field>
                        {errors.email && touched.email ? <div className="validation">{errors.email}</div> : null}
                    </span>
                    <span>
                        <label>Confirm new email address</label>
                        <Field type="text" name="confirmEmail" placeholder="Enter email again"></Field>
                        {errors.confirmEmail && touched.confirmEmail ? <div className="validation">{errors.confirmEmail}</div> : null}
                    </span>
                    <button className="profile__switch__button" type="submit">
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

export default EmailChangeForm
