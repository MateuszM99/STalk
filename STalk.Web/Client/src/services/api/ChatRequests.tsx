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

}export function getFile(fileId){
    return axios.get(`${baseUrl}/chat/getFile?fileId=${fileId}`);
}

export function saveFileAndAdd(data) {
    return axios.post(`${baseUrl}/chat/saveFile`, data, { headers: { 'Content-Type': 'multipart/form-data' }});
}