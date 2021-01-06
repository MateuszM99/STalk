import React from 'react';
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import './style.scss'

function PasswordResetWindow() {

    return (
        <div className="limiter">
		<div className="container-reset">
			<div className="wrap-reset">
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
