import axios from 'axios'

const baseUrl = 'https://localhost:44338/api';

// const token = JSON.parse(localStorage.getItem('userData')).token;

// if(token != null){
//     console.log();
//     axios.interceptors.request.use(
//         config => {
//         config.headers.authorization = `Bearer ${token}`;
//         return config;
//         },
//         error => {
//         return Promise.reject(error);
//         }
//     );
// }

export function emailChangeRequest(emailData){
    return axios.post(`${baseUrl}/user/emailChange`,emailData);
}

export function profileImageChangeRequest(profileImageData){
    const config = {
        headers: { 'content-type': 'multipart/form-data' }
    }
    let formData = new FormData();
    formData.append('profileImage',profileImageData.profileImage);
    return axios.post(`${baseUrl}/user/profileImageChange`,formData,config);
}

export function usernameChangeRequest(usernameData){
    return axios.post(`${baseUrl}/user/usernameChange`,usernameData);
}

export function passwordChangeRequest(passwordData){
    return axios.post(`${baseUrl}/user/passwordChange`,passwordData);
}

export function getProfileRequest(){
    return axios.get(`${baseUrl}/user/getUser`);
}