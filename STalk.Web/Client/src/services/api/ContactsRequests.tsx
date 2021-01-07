import axios from 'axios'

const baseUrl = 'https://localhost:44333/api';

export function getUsersRequest(signInData){
    return axios.post(`${baseUrl}/user/getUsers`,signInData);
}