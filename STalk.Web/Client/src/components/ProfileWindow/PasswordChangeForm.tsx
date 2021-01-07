import React from 'react'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'

function PasswordChangeForm() {
    return (
        <div className="profile__edit__form">
            <Formik 
            initialValues={{

            }}

            validationSchema = {Yup.object({
                
            })}

            onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) =>  {   }}
            >
            {({ errors,touched,isSubmitting,status}) => (
                <Form>
                    <span>
                        <label>Enter old password</label>
                        <input type="text"></input>
                    </span>
                    <span>
                        <label>Enter new password</label>
                        <input type="text"></input>
                    </span>
                    <span>
                        <label>Confirm new password</label>
                        <input type="text"></input>
                    </span>
                    <button className="profile__switch__button">Save changes</button>
                </Form>
            )}
            </Formik>
        </div>
    )
}

export default PasswordChangeForm
