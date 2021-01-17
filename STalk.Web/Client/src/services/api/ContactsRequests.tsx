import axios from 'axios'

const baseUrl = 'https://localhost:44338/api';

// const token = JSON.parse(localStorage.getItem('userData'));

// if(token.token != null){
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

export function getUsersRequest(searchString){
    return axios.get(`${baseUrl}/user/getUsers?searchString=${searchString}`);
}

export function getUsersContactsRequest(){
    return axios.get(`${baseUrl}/user/getUsersContacts`);
}

export function getUsersFriendsRequestsRequest(){
    return axios.get(`${baseUrl}/user/getUsersFriendRequests`);
}

export function addToContactsRequest(username){
    return axios.post(`${baseUrl}/user/addToContacts`,{String : username});
}

export function acceptAddToContactsRequest(addRequestId){
    return axios.post(`${baseUrl}/user/acceptAddToContacts`,{Long : addRequestId});
}

export function declineAddToContactsRequest(addRequestId){
    return axios.post(`${baseUrl}/user/declineAddToContacts`,{Long : addRequestId});
}