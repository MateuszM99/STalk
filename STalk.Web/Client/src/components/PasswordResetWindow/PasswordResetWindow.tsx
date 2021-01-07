import React from 'react';
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import './style.scss'
import {passwordChangeRequest} from '../../services/api/AuthRequests'
import { useHistory } from "react-router";

function PasswordResetWindow() {
    const history = useHistory();
    return (
        <div className="limiter">
		<div className="container-reset">
			<div className="wrap-reset">
				<Formik
                initialValues={{
                    password : '',
                    confirmPassword : ''
                }}
                
                validationSchema = {Yup.object({
                    password : Yup.string(),
                    confirmPassword : Yup.string(),
                })}

                onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) => {
                    if(values.password && values.confirmPassword){
                        try{                           
                            let response = await passwordChangeRequest(values);
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
                        <span className="reset-form-title">
                            Reset your password
                        </span>
                        <h6>New password</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="password" name="password" placeholder="Password"/>
                            {errors.password && touched.password ? <div className="validation">{errors.password}</div> : null}
                        </div>
                        <h6>Confirm new password</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="password" name="password" placeholder="Password"/>
                            {errors.password && touched.password ? <div className="validation">{errors.password}</div> : null}
                        </div>
                        <div className="container-reset-form-btn">
                            <button className="reset-form-btn">
                                Change password
                            </button>
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

export default PasswordResetWindow
