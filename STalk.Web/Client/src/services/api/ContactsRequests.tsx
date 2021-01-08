import axios from 'axios'

const baseUrl = 'https://localhost:44338/api';

export function getUsersRequest(searchString){
    return axios.get(`${baseUrl}/user/getUsers?searchString=${searchString}`);
}

export function getUsersContactsRequest(){
    return axios.get(`${baseUrl}/user/getUsersContacts`);
}

export function getUsersFriendsRequestsRequest(){
    return axios.get(`${baseUrl}/user/getUsersFriendsRequests`);
}
