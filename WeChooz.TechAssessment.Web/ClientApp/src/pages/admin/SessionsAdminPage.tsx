import {useSessionsAdminPage} from "./hooks/useSessionsAdminPage.tsx";
import {SessionFromAdminListingPageDto} from "../../../api/session/queries/get-sessions-from-admin-listing-page-query.tsx";
import AdminSessionsTable from "./components/AdminSessionsTable.tsx";
import {Center, Loader} from "@mantine/core";
import {useState} from "react";
import SessionAddEditModal from "./components/SessionAddEditModal.tsx";


const SessionsAdminPage = () => {
    const {data, isLoading, isError, error} = useSessionsAdminPage();
    const [selectedSession, setSelectedSession] = useState<SessionFromAdminListingPageDto | null>(null);

    const handleEditSave = (session: SessionFromAdminListingPageDto) => {
        // update Session using Mutation
        setSelectedSession(null);
    };

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
            onSave={handleEditSave}
        />
    </>;
};

export default SessionsAdminPage;