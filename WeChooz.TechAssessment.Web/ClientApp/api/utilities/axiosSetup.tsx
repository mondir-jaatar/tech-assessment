import axios from 'axios';
import {notifications} from "@mantine/notifications";

const tenantAxios = axios.create({
    baseURL: import.meta.env.VITE_API_BACK_END_URL
});
tenantAxios.defaults.headers.Accept = 'application/json';

const privateAxios = axios.create({
    baseURL: import.meta.env.VITE_API_BACK_END_URL,
    withCredentials: true,
});
privateAxios.interceptors.response.use(
    response => response,
    error => {
        if (error.response?.status === 401) {
            window.location.href = "/authentication";
        }
        else if (error.response?.status === 405) {
            notifications.show({
                message: "Vous n’êtes pas autorisé à faire cette opération",
                color: 'red',
                autoClose: 5000,
            });
        }
        return Promise.reject(error);
    }
);
privateAxios.defaults.headers.Accept = 'application/json';


export {tenantAxios, privateAxios};
