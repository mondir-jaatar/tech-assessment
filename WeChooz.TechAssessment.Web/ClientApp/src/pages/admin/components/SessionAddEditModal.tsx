import {SessionFromAdminListingPageDto} from "../../../../api/session/queries/get-sessions-from-admin-listing-page-query.tsx";
import {useForm} from "@mantine/form";
import {Button, Group, Modal, NumberInput, TextInput} from "@mantine/core";

interface SessionAddEditModalProps {
    session?: SessionFromAdminListingPageDto | null;
    opened: boolean;
    onClose: () => void;
    onSave: (session: any) => void; //TODO update the any type by the command type
}

const SessionAddEditModal = ({
                                        session,
                                        opened,
                                        onClose,
                                        onSave,
                                    }: SessionAddEditModalProps) => {
    
    const form = useForm({
        initialValues: {
            name: session?.course.name ?? "",
            duration: session?.duration ?? 0,
        },
        validate: {
            name: (value) => (value.trim().length === 0 ? "Nom requis" : null),
            duration: (value) => (value <= 0 ? "Durée invalide" : null),
        },
    });

    const handleSubmit = (values: typeof form.values) => {
        if (!session)
        {
            //TODO handle add new session
            return;
        }

        onSave(form.values);
        onClose();
    };

    return (
        <Modal
            opened={opened}
            onClose={onClose}
            title={session ? "Editer la session" : "Ajouter une session"}
            centered
        >
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <TextInput
                    label="Nom de la formation"
                    {...form.getInputProps("name")}
                    mb="sm"
                />
                <NumberInput
                    label="Durée (minutes)"
                    {...form.getInputProps("duration")}
                    mb="sm"
                    min={0}
                />
                <Group mt="md">
                    <Button variant="outline" onClick={onClose}>
                        Annuler
                    </Button>
                    <Button type="submit">{session ? "Sauvegarder" : "Ajouter"}</Button>
                </Group>
            </form>
        </Modal>
    );
};

export default SessionAddEditModal;