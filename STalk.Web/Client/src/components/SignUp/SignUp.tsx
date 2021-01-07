import React from 'react';
import './style.scss';
import './util.scss';
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import {signUpRequest} from '../../services/api/AuthRequests'
import { useHistory } from "react-router";

function SignUp() {
    const history = useHistory();
    return (
        <div className="limiter">
		<div className="container-signUp">
			<div className="wrap-signUp">
				<div className="signUp-pic js-tilt" data-tilt>
					<img src="/img-01.png" alt="IMG"/>
				</div>
				<Formik
                initialValues={{
                    email : '',
                    username : '',
                    password : '',
                    confirmPassword : ''
                }}
                

                onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) => {
                    console.log(values)
                    if(values.username && values.password){
                        try{
                            let response = await signUpRequest(values);
                            setSubmitting(false);
                            resetForm();
                            history.push({
                                pathname : "/signIn"
                            })
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
                        <span className="signUp-form-title">
                            Sign up
                        </span>
                        <h6>Email</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="text" name="email" placeholder="Email"/>
                            {errors.email && touched.email ? <div className="validation">{errors.email}</div> : null}
                        </div>
                        <h6>Username</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="text" name="username" placeholder="Password"/>
                            {errors.username && touched.username ? <div className="validation">{errors.username}</div> : null}
                        </div>
                        <h6>Password</h6>
                        <div className="wrap-input100">                           
                            <Field className="input100" type="password" name="password" placeholder="Password"/>
                            {errors.password && touched.password ? <div className="validation">{errors.password}</div> : null}
                        </div>
                        <h6>Confirm Password</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="password" name="confirmPassword" placeholder="Password"/>
                            {errors.confirmPassword && touched.confirmPassword ? <div className="validation">{errors.confirmPassword}</div> : null}
                        </div>
                        <div className="container-signUp-form-btn">
                            <button className="signUp-form-btn" type="submit">
                            {isSubmitting ? 'Signin up ...' : 'Sign up'}
                            </button>
                            {status && status.errorMessage ? (
                                <div className="validation">{status.errorMessage}</div>
                            ) : null}
                        </div>
                        <div className="text-center p-t-136">
                            <a className="txt2" href="/signIn">
                                Go to login page                               
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

export default SignUp
