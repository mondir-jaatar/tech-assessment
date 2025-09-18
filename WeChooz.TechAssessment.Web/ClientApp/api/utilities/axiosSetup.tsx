import axios from 'axios';

const tenantAxios = axios.create();
tenantAxios.defaults.headers.Accept = 'application/json';

export default tenantAxios;
