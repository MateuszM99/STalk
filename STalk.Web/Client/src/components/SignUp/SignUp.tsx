import React from 'react';
import './style.scss';
import './util.scss';
import {Formik,Form, yupToFormErrors,Field} from 'formik'


function SignUp() {
    return (
        <div className="limiter">
		<div className="container-signUp">
			<div className="wrap-signUp">
				<div className="signUp-pic js-tilt" data-tilt>
					<img src="/img-01.png" alt="IMG"/>
				</div>
				<Formik
                initialValues={{
                    username : '',
                    password : '',
                }}
                

                onSubmit = {() => {
                    
                }}
                >
                {({ errors, touched,isSubmitting,status}) => (
                    <Form>
                        <span className="signUp-form-title">
                            Sign up
                        </span>
                        <h6>Email</h6>
                        <div className="wrap-input100 validate-input" data-validate = "Valid email is required: ex@abc.xyz">
                            <input className="input100" type="text" name="email" placeholder="Email"/>
                        </div>
                        <h6>Username</h6>
                        <div className="wrap-input100 validate-input" data-validate = "Password is required">
                            <input className="input100" type="password" name="pass" placeholder="Password"/>
                        </div>
                        <h6>Password</h6>
                        <div className="wrap-input100 validate-input" data-validate = "Password is required">                           
                            <input className="input100" type="password" name="pass" placeholder="Password"/>
                        </div>
                        <h6>Confirm Password</h6>
                        <div className="wrap-input100 validate-input" data-validate = "Password is required">
                            <input className="input100" type="password" name="pass" placeholder="Password"/>
                        </div>
                        <div className="container-signUp-form-btn">
                            <button className="signUp-form-btn">
                                Sign up
                            </button>
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
