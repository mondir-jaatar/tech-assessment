import { useEffect, useState } from "react";
import { Button, Modal, Stack, TextInput, Group, ActionIcon } from "@mantine/core";
import { IconTrash } from "@tabler/icons-react";
import { SessionParticipantFromSessionAdminDto } from "../../../../../api/session/queries/get-session-participants-from-admin-listing-page-query.tsx";
import { useSessionParticipantsPage } from "../hooks/useSessionParticipants.tsx";

export interface ParticipantsModalProps {
    sessionId: string;
    opened: boolean;
    onClose: () => void;
}

export const ParticipantsModal = ({ sessionId, opened, onClose }: ParticipantsModalProps) => {
    const { updateParticipantsMutation, participantsQuery } = useSessionParticipantsPage({ sessionId });
    const [localParticipants, setLocalParticipants] = useState<SessionParticipantFromSessionAdminDto[]>([]);

    useEffect(() => {
        if (participantsQuery.data?.data) {
            setLocalParticipants(participantsQuery.data.data);
        }
    }, [participantsQuery.data]);

    const handleChange = (index: number, key: "firstName" | "lastName" | "companyName" | "email", value: string) => {
        setLocalParticipants((prev) =>
            prev.map((p, i) => (i === index ? { ...p, [key]: value } : p))
        );
    };

    const handleDelete = (index: number) => {
        setLocalParticipants((prev) => prev.filter((_, i) => i !== index));
    };

    const handleAdd = () => {
        setLocalParticipants((prev) => [
            ...prev,
            { firstName: "", lastName: "", companyName: "", email: "" } as SessionParticipantFromSessionAdminDto,
        ]);
    };

    const handleSave = () => {
        updateParticipantsMutation.mutate(
            { sessionId, participants: localParticipants },
            { onSuccess: onClose }
        );
    };

    return (
        <Modal opened={opened} onClose={onClose} title="Participants" size="lg">
            <Stack>
                {localParticipants.map((p, index) => (
                    <Group key={index} align="flex-start">
                        <TextInput
                            placeholder="Prénom"
                            value={p.firstName}
                            onChange={(e) => handleChange(index, "firstName", e.currentTarget.value)}
                            style={{ flex: 1 }}
                        />
                        <TextInput
                            placeholder="Nom"
                            value={p.lastName}
                            onChange={(e) => handleChange(index, "lastName", e.currentTarget.value)}
                            style={{ flex: 1 }}
                        />
                        <TextInput
                            placeholder="Email"
                            value={p.email}
                            onChange={(e) => handleChange(index, "email", e.currentTarget.value)}
                            style={{ flex: 1 }}
                        />
                        <TextInput
                            placeholder="Nom de l'entreprise"
                            value={p.companyName}
                            onChange={(e) => handleChange(index, "companyName", e.currentTarget.value)}
                            style={{ flex: 1 }}
                        />
                        <ActionIcon
                            color="red"
                            onClick={() => handleDelete(index)}
                            style={{ flex: "0 0 40px", marginTop: 3 }}
                        >
                            <IconTrash size={16} />
                        </ActionIcon>
                    </Group>
                ))}

                <Button onClick={handleAdd} variant="light">
                    Ajouter un participant
                </Button>

                <Group justify="flex-end" mt="md">
                    <Button variant="outline" onClick={onClose}>
                        Annuler
                    </Button>
                    <Button onClick={handleSave} loading={updateParticipantsMutation.isPending}>
                        Enregistrer
                    </Button>
                </Group>
            </Stack>
        </Modal>
    );
};