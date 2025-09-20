import { useQuery } from '@tanstack/react-query';
import {SessionFromAdminListingPageDto} from "../../../../api/session/queries/get-sessions-from-admin-listing-page-query.tsx";
import {SessionService} from "../../../../api/session/session-service.tsx";
import {Response} from "../../../../api/utilities/model/Response.tsx";

export const useSessionsAdminPage = () => {
    return useQuery<Response<SessionFromAdminListingPageDto[]>, Error>({
        queryKey: ['adminListingPage'],
        queryFn: () => SessionService.GetFromAdminListingPage({}),
    });
};
