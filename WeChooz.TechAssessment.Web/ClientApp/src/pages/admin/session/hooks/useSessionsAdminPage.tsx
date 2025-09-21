import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { SessionFromAdminListingPageDto } from "../../../../../api/session/queries/get-sessions-from-admin-listing-page-query";
import { SessionService } from "../../../../../api/session/session-service";
import { Response } from "../../../../../api/utilities/model/Response";
import {UpdateSessionCommand} from "../../../../../api/session/commands/update-session-command.tsx";
import {notifications} from "@mantine/notifications";

export const useSessionsAdminPage = () => {
    const queryClient = useQueryClient();

    const sessionsQuery = useQuery<Response<SessionFromAdminListingPageDto[]>, Error>({
        queryKey: ['adminListingPage'],
        queryFn: () => SessionService.GetFromAdminListingPage({}),
    });

    const updateSessionMutation = useMutation<
        Response<SessionFromAdminListingPageDto>,
        Error,
        UpdateSessionCommand
    >({
        mutationFn: (session) => SessionService.Update(session),
        onSuccess: () => {
            notifications.show({
                message: 'La session a été mise à jour',
                color: 'green',
                autoClose: 5000,
            });
            queryClient.invalidateQueries({ queryKey: ['adminListingPage'] });
        }
    });

    return { sessionsQuery, updateSessionMutation };
};
