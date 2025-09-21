import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";
import {Response} from "../../../../../api/utilities/model/Response.tsx";
import {GetSessionParticipantsFromAdminPageQuery, SessionParticipantFromSessionAdminDto} from "../../../../../api/session/queries/get-session-participants-from-admin-listing-page-query.tsx";
import {SessionService} from "../../../../../api/session/session-service.tsx";
import { UpdateParticipantsCommand} from "../../../../../api/session/commands/update-participants-command.tsx";
import {notifications} from "@mantine/notifications";

export const useSessionParticipantsPage = (query: GetSessionParticipantsFromAdminPageQuery) => {
    const queryClient = useQueryClient();

    const participantsQuery = useQuery<Response<SessionParticipantFromSessionAdminDto[]>, Error>({
        queryKey: ['participantsAdminListingPage', query],
        queryFn: () => SessionService.GetParticipantsFromSessionAdminPage(query),
    });

    const updateParticipantsMutation = useMutation({
        mutationFn: (command: UpdateParticipantsCommand) => SessionService.UpdateParticipants(command),
        onSuccess: () => {
            notifications.show({
                message: 'La liste des participants a été mise à jour',
                color: 'green',
                autoClose: 5000,
            });
            queryClient.invalidateQueries({
                queryKey: ["participantsAdminListingPage", query],
            });
        },
    });

    return {participantsQuery, updateParticipantsMutation};
}