import axios from 'axios';

const tenantAxios = axios.create({
    baseURL: import.meta.env.VITE_API_BACK_END_URL
});
tenantAxios.defaults.headers.Accept = 'application/json';

const privateAxios = axios.create({
    baseURL: import.meta.env.VITE_API_BACK_END_URL,
    withCredentials: true,
});
privateAxios.defaults.headers.Accept = 'application/json';


export default {tenantAxios, privateAxios};
