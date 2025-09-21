import {tenantAxios, privateAxios} from '../utilities/axiosSetup';
import {handleError, handleResponse} from "../utilities/Response.tsx";
import {GetSessionsFromPublicListingPageQuery} from "./queries/get-sessions-from-public-listing-page-query.tsx";
import {GetSessionsFromAdminListingPageQuery} from "./queries/get-sessions-from-admin-listing-page-query.tsx";
import {UpdateSessionCommand} from "./commands/update-session-command.tsx";
import {UpdateParticipantsCommand} from "./commands/update-participants-command.tsx";
import {GetSessionParticipantsFromAdminPageQuery} from "./queries/get-session-participants-from-admin-listing-page-query.tsx";

const BACK_END_URL = 'api/v1/Session';

export class SessionService {
    public static GetFromPublicListingPage = (query: GetSessionsFromPublicListingPageQuery) => {
        return tenantAxios
            .get(`${BACK_END_URL}/get-from-public-listing-page`, { params: query })
            .then(handleResponse)
            .catch(handleError);
    };

    public static GetFromAdminListingPage = (query: GetSessionsFromAdminListingPageQuery) => {
        return privateAxios
            .get(`${BACK_END_URL}/get-from-admin-listing-page`, { params: query })
            .then(handleResponse)
            .catch(handleError);
    };

    public static Update = (command: UpdateSessionCommand) => {
        return privateAxios.put(BACK_END_URL, command)
            .then(handleResponse)
            .catch(handleError);
    };

    public static GetParticipantsFromSessionAdminPage = (query: GetSessionParticipantsFromAdminPageQuery) => {
        return privateAxios
            .get(`${BACK_END_URL}/get-participants-from-session-admin-page`, { params: query })
            .then(handleResponse)
            .catch(handleError);
    };

    public static UpdateParticipants = (command: UpdateParticipantsCommand) => {
        return privateAxios.put(`${BACK_END_URL}/participants`, command)
            .then(handleResponse)
            .catch(handleError);
    };
}

