import React from 'react';
import './style.scss';
import './util.scss';
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import {signUpRequest} from '../../services/api/AuthRequests'
import { useHistory,Redirect } from "react-router";
import * as Yup from 'yup'


function SignUp() {
    const history = useHistory();

    if(localStorage.getItem('userData') != null){
        return (
            <Redirect to="/sTalk/chat"/>
        )
    }

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
                
                validationSchema = {Yup.object({
                    email : Yup.string()
                    .required('Email is required')
                    .email('Email is not valid'),
                    username : Yup.string()
                        .required('Username is required'),
                    password : Yup.string()
                        .required('Password is required')
                        .matches(
                            /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/,
                            "Must contain 8 characters, one uppercase, one lowercase, one number and one special case character"
                          ),
                    confirmPassword: Yup.string().required('Password confirm is required')
                    .oneOf([Yup.ref("password"), null], "Passwords must match")
                })}

                onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) => {
                    console.log(values)
                    if(values.username && values.password){
                        try{
                            let response = await signUpRequest(values);
                            setSubmitting(false);
                            resetForm();
                            history.push("/signIn");
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
