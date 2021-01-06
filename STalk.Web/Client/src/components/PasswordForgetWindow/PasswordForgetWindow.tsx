import React from 'react';
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import './style.scss'

function PasswordForgetWindow() {

    return (
        <div className="limiter">
		<div className="container-forgot">
			<div className="wrap-forgot">
				<Formik
                initialValues={{
                    username : '',
                    password : '',
                }}
                
                validationSchema = {Yup.object({
                    username : Yup.string()
                        .required('Username is required'),
                    password : Yup.string()
                        .required('Password is required'),
                })}

                onSubmit = {() => {
                    
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
                            {errors.password && touched.password ? <div className="validation">{errors.password}</div> : null}
                        </div>
                        <div className="container-forgot-form-btn">
                            <button className="forgot-form-btn">
                                Send
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

export default PasswordForgetWindow
