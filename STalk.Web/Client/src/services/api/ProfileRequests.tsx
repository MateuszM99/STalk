import axios from 'axios'

const baseUrl = 'https://localhost:44338/api';

export function emailChangeRequest(emailData){
    return axios.post(`${baseUrl}/user/emailChange`,emailData);
}

export function profileImageChangeRequest(profileImageData){
    return axios.post(`${baseUrl}/user/profileImageChange`,profileImageData);
}

export function usernameChangeRequest(usernameData){
    return axios.post(`${baseUrl}/user/usernameChange`,usernameData);
}

export function passwordChangeRequest(passwordData){
    return axios.post(`${baseUrl}/user/passwordChange`,passwordData);
}