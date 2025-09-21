import {useSessionsAdminPage} from "./hooks/useSessionsAdminPage.tsx";
import AdminSessionsTable from "./components/AdminSessionsTable.tsx";
import {Center, Loader} from "@mantine/core";
import {useState} from "react";
import SessionAddEditModal from "./components/SessionAddEditModal.tsx";
import {SessionFromAdminListingPageDto} from "../../../../api/session/queries/get-sessions-from-admin-listing-page-query.tsx";
import {ParticipantsModal} from "./components/ParticipantsModal.tsx";


const SessionsAdminPage = () => {
    const {sessionsQuery, updateSessionMutation} = useSessionsAdminPage();
    const [selectedSession, setSelectedSession] = useState<SessionFromAdminListingPageDto | null>(null);
    const [selectedSessionId, setSelectedSessionId] = useState<string | null>(null);

    if (sessionsQuery.isLoading) {
        return <Center py="xl">
            <Loader size="lg" variant="dots"/>
        </Center>;
    }

    return <>
        <AdminSessionsTable sessions={sessionsQuery.data?.data ?? []} onEdit={(session) => setSelectedSession(session)} onEditParticipants={(sessionId) => setSelectedSessionId(sessionId)}/>

        <SessionAddEditModal
            session={selectedSession}
            opened={!!selectedSession}
            onClose={() => setSelectedSession(null)}
            onUpdate={(session) => updateSessionMutation.mutate(session)}
        />

        {
            selectedSessionId && <ParticipantsModal
                sessionId={selectedSessionId!}
                opened={!!selectedSessionId}
                onClose={() => setSelectedSessionId(null)}
            />
        }
    </>;
};

export default SessionsAdminPage;