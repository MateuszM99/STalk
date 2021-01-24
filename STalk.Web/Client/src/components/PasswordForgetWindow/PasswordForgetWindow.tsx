import React from 'react';
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import './style.scss'
import {passwordRetrieveRequest} from '../../services/api/AuthRequests'
import { Redirect } from "react-router";

function PasswordForgetWindow() {

    const token = JSON.parse(localStorage.getItem('userData')) == null ? null : JSON.parse(localStorage.getItem('userData')).token;
    if(token != null){
        return (
            <Redirect to="/sTalk/chat"/>
        )
    }

    return (
        <div className="limiter">
		<div className="container-forgot">
			<div className="wrap-forgot">
				<Formik
                initialValues={{
                    email : ''
                }}
                
                validationSchema = {Yup.object({
                    email : Yup.string()
                    .required('You must enter an email')
                    .email('email is not valid'),
                })}

                onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) => {
                    if(values.email){
                        try{
                            let response = await passwordRetrieveRequest(values);
                            setSubmitting(false);
                            resetForm();
                        } catch(err){
                            console.log(err.message);
                            setSubmitting(false);
                            resetForm();
                            setStatus({
                                errorMessage : err.message
                            });
                        }                                                                                                                                                                                        
                    }  
                }}
                >
                {({ errors, touched,isSubmitting,status}) => (
                    <Form>
                        <span className="forgot-form-title">
                            Forgot your password? Enter your email
                        </span>
                        <h6>Email</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="text" name="email" placeholder="Enter your email"/>
                            {errors.email && touched.email ? <div className="validation">{errors.email}</div> : null}
                        </div>
                        <div className="container-forgot-form-btn">
                            <button className="forgot-form-btn" type="submit">
                                {isSubmitting ? 'Sending ...' : 'Send'}
                            </button>
                            {status && status.errorMessage ? (
                                <div className="validation">{status.errorMessage}</div>
                            ) : null}
                        </div>
                        <div className="text-center">
                            <a className="txt2" href="/signIn">
                                Go to sign in page                                
                            </a>
                        </div>
                    </Form>
                )}
                </Formik>
			</div>
		</div>
	</div>
    )
}

export default PasswordForgetWindow
