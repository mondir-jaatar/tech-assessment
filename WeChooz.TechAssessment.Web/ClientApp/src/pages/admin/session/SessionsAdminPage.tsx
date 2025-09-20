import {useSessionsAdminPage} from "./hooks/useSessionsAdminPage.tsx";
import AdminSessionsTable from "./components/AdminSessionsTable.tsx";
import {Center, Loader} from "@mantine/core";
import {useState} from "react";
import SessionAddEditModal from "./components/SessionAddEditModal.tsx";
import {SessionFromAdminListingPageDto} from "../../../../api/session/queries/get-sessions-from-admin-listing-page-query.tsx";


const SessionsAdminPage = () => {
    const {data, isLoading, isError, error, updateSessionMutation } = useSessionsAdminPage();
    const [selectedSession, setSelectedSession] = useState<SessionFromAdminListingPageDto | null>(null);

    if (isLoading) {
        return <Center py="xl">
            <Loader size="lg" variant="dots"/>
        </Center>;
    }

    return <>
        <AdminSessionsTable sessions={data?.data ?? []} onEdit={(session) => setSelectedSession(session)} />

        <SessionAddEditModal
            session={selectedSession}
            opened={!!selectedSession}
            onClose={() => setSelectedSession(null)}
            onUpdate={(session) => updateSessionMutation.mutate(session)}
        />
    </>;
};

export default SessionsAdminPage;