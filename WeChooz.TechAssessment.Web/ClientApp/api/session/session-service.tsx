import tenantAxios from '../utilities/axiosSetup';
import {handleError, handleResponse} from "../utilities/Response.tsx";
import {GetSessionsFromPublicListingPageQuery} from "./queries/get-sessions-from-public-listing-page-query.tsx";
const BACK_END_URL = `${import.meta.env.VITE_API_BACK_END_URL}/api/v1/Session`;
export class SessionService {
    public static GetFromPublicListingPage = (query: GetSessionsFromPublicListingPageQuery) => {
        return tenantAxios
            .get(`${BACK_END_URL}/get-from-public-listing-page`, { params: query })
            .then(handleResponse)
            .catch(handleError);
    };
}

