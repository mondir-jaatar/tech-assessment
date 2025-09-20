import {PerformLoginRequest} from "./queries/perform-login-request.tsx";
import {handleError, handleResponse} from "../utilities/Response.tsx";
import {privateAxios} from "../utilities/axiosSetup.tsx";

const BACK_END_URL = '_api/admin/login';

export type Claim = {
    type: string;
    value: string;
};

export class AuthenticationService {
    public static Login = (request: PerformLoginRequest) => {
        return privateAxios
            .post(BACK_END_URL, request)
            .then((data) => data.data)
            .catch(handleError);
    }
}