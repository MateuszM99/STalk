import axios from 'axios'

const baseUrl = 'https://localhost:44338/api';

 const token = JSON.parse(localStorage.getItem('userData')) == null ? null : JSON.parse(localStorage.getItem('userData')).token;

 if(token != null){
     console.log();
     axios.interceptors.request.use(
         config => {
         config.headers.authorization = `Bearer ${token}`;
         return config;
         },
         error => {
         return Promise.reject(error);
         }
     );
 }

export function getConversationRequest(userId){
    return axios.get(`${baseUrl}/chat/getConversation?userId2=${userId}`);
}