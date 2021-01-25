import React from 'react';
import './style.scss';
import './util.scss';
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import {signInRequest} from '../../services/api/AuthRequests'
import * as Yup from 'yup'
import { useHistory,Redirect } from "react-router";
import axios from 'axios'

function SignIn() {
    /*const setAxiosInterceptors = (userData) => {
        axios.interceptors.request.use(function (config) {
            const token = userData.token;
            config.headers.Authorization =  token;
        
            return config;
        });
    }*/
    const setAxiosToken = (token) => {
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}` 
    }

    const history = useHistory();    
    const token = null;
    console.log(token);
    if(token != null){
        return (
            <Redirect to="/sTalk/chat"/>
        )
    }

    return (
        <div className="limiter">
		<div className="container-signIn">
			<div className="wrap-signIn">
				<div className="signIn-pic js-tilt" data-tilt>
					<img src="/img-01.png" alt="IMG"/>
				</div>
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

                onSubmit = {async (values,{setSubmitting, setStatus,resetForm}) =>  {                                       
                        if(values.username && values.password){
                            try{
                                let response = await signInRequest(values);
                                localStorage.setItem('userData',JSON.stringify(response.data));
                                setAxiosToken(response.data.token);
                                //setAxiosInterceptors(response.data);
                                setSubmitting(false);
                                resetForm();
                                history.push("/sTalk/chat/");
                            } catch(err){
                                setSubmitting(false);
                                resetForm();
                                setStatus({
                                    errorMessage : "Looks like either your username or password were incorrect. Wanna try again?"
                                });
                            }                                                                                                                                                                                        
                        }                                  
                }}
                >
                {({ errors,touched,isSubmitting,status}) => (
                    <Form>
                        <span className="signIn-form-title">
                            Sign in
                        </span>
                        <h6>Username</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="text" name="username" placeholder="Username"/>
                            {errors.username && touched.username ? <div className="validation">{errors.username}</div> : null}                                                                       
                        </div>
                        <h6>Password</h6>
                        <div className="wrap-input100">
                            <Field className="input100" type="password" name="password" placeholder="Password"/>
                            {errors.password && touched.password ? <div className="validation">{errors.password}</div> : null}
                        </div>
                        <div className="container-signIn-form-btn">
                            <button className="signIn-form-btn" type="submit">                                                           
                                {isSubmitting ? 'Signin in ...' : 'Sign in'}
                            </button>
                            {status && status.errorMessage ? (
                                <div className="validation">{status.errorMessage}</div>
                            ) : null}
                        </div>
                        <div className="text-center p-t-12">
                            <span className="txt1">
                                Forgot
                            </span>
                            <a className="txt2" href="/forgot">
                                Username / Password?
                            </a>
                        </div>
                        <div className="text-center p-t-136">
                            <a className="txt2" href="/signUp">
                                Create your Account                                
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

export default SignIn
