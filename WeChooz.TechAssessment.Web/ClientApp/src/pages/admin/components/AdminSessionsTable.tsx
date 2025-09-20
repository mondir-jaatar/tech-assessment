import {ActionIcon, ScrollArea, Table, Tooltip} from "@mantine/core";
import {SessionFromAdminListingPageDto} from "../../../../api/session/queries/get-sessions-from-admin-listing-page-query.tsx";
import {DeliveryMode} from "../../../../api/enums/delivery-mode.tsx";
import {IconEdit} from "@tabler/icons-react";
interface AdminSessionsTableProps {
    sessions: SessionFromAdminListingPageDto[];
    onEdit: (session: SessionFromAdminListingPageDto) => void;
}

const AdminSessionsTable = ({ sessions, onEdit }: AdminSessionsTableProps) => {
    const rows = sessions.map((session) => (
        <Table.Tr key={session.id}>
            <Table.Td>{session.course.name}</Table.Td>
            <Table.Td>{new Date(session.startDate).toLocaleDateString('fr-FR')}</Table.Td>
            <Table.Td>
                {session.trainer.firstName} {session.trainer.lastName.toUpperCase()}
            </Table.Td>
            <Table.Td>
                {session.deliveryMode === DeliveryMode.OnSite ? 'Présentiel' : 'À distance'}
            </Table.Td>
            <Table.Td>
                {session.duration >= 60
                    ? `${Math.floor(session.duration / 60)} h ${session.duration % 60} min`
                    : `${session.duration} min`}
            </Table.Td>
            <Table.Td>
                <Tooltip label="Editer la session">
                    <ActionIcon color="blue" variant="light" onClick={() => onEdit(session)}>
                        <IconEdit size={16} />
                    </ActionIcon>
                </Tooltip>
            </Table.Td>
        </Table.Tr>
    ));

    return (
        <ScrollArea>
            <Table highlightOnHover striped>
                <Table.Thead>
                    <Table.Tr>
                        <Table.Th>Formation</Table.Th>
                        <Table.Th>Date</Table.Th>
                        <Table.Th>Formateur</Table.Th>
                        <Table.Th>Mode</Table.Th>
                        <Table.Th>Durée</Table.Th>
                        <Table.Th>Actions</Table.Th>
                    </Table.Tr>
                </Table.Thead>
                <Table.Tbody>{rows}</Table.Tbody>
            </Table>
        </ScrollArea>
    );
};

export default AdminSessionsTable;