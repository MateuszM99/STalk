import axios from 'axios'

const token = JSON.parse(localStorage.getItem('userData')).token

const header = `Authorization: Bearer ${token}`;


const baseUrl = 'https://localhost:44338/api';

export function getUsersRequest(searchString){
    return axios.get(`${baseUrl}/user/getUsers?searchString=${searchString}`);
}

export function getUsersContactsRequest(){
    return axios.get(`${baseUrl}/user/getUsersContacts`,{ headers: {"Authorization" : `Bearer ${token}`} });
}

export function getUsersFriendsRequestsRequest(){
    return axios.get(`${baseUrl}/user/getUsersFriendRequests`);
}
