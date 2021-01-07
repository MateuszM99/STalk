import axios from 'axios'

const baseUrl = 'https://localhost:44333/api';

export function signInRequest(signInData){
    return axios.post(`${baseUrl}/authentication/signIn`,signInData);
}

export function signUpRequest(signUpData){
    return axios.post(`${baseUrl}/authentication/signUp`,signUpData);
}

export function passwordRetrieveRequest(emailData){
    return axios.post(`${baseUrl}/authentication/sendPasswordReset`,emailData);
}

export function passwordChangeRequest(passwordData){
    return axios.post(`${baseUrl}/authentication/resetPassword`,passwordData);
}
