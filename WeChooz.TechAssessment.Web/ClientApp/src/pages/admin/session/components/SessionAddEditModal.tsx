// SessionAddEditModal.tsx
import {useForm} from "@mantine/form";
import {Button, Group, Modal, NumberInput, Select} from "@mantine/core";
import {DateTimePicker} from "@mantine/dates";
import {SessionFromAdminListingPageDto} from "../../../../../api/session/queries/get-sessions-from-admin-listing-page-query.tsx";
import {useCoursesDictionary} from "../hooks/useCoursesDictionary.tsx";
import {DeliveryMode} from "../../../../../api/enums/delivery-mode.tsx";
import {CourseSelectItem} from "./CourseSelectItem.tsx";
import {UpdateSessionCommand} from "../../../../../api/session/commands/update-session-command.tsx";
import {useEffect} from "react";

interface SessionAddEditModalProps {
    session?: SessionFromAdminListingPageDto | null;
    opened: boolean;
    onClose: () => void;
    onUpdate: (session: UpdateSessionCommand) => void;
}

const SessionAddEditModal = ({session, opened, onClose, onUpdate}: SessionAddEditModalProps) => {
    const {courses, list} = useCoursesDictionary();

    const form = useForm({
        initialValues: {
            id: "",
            startDate: new Date(),
            deliveryMode: "OnSite",
            duration: 60,
            courseId: "",
            rowVersion: "",
        },
        validate: {
            startDate: (value) => (value ? null : "Date requise"),
            deliveryMode: (value) => (value ? null : "Le mode de délivrance requis"),
            duration: (value) => (value > 0 ? null : "Durée invalide"),
            courseId: (value) => (value ? null : "Formation requise"),
        },
    });

    useEffect(() => {
        if (session) {
            form.setValues({
                id: session.id,
                startDate: new Date(session.startDate),
                deliveryMode: session.deliveryMode === DeliveryMode.OnSite ? "OnSite" : "Remote",
                duration: session.duration,
                courseId: session.course.id,
                rowVersion: session.rowVersion,
            });
        } else {
            form.setValues({
                startDate: new Date(),
                deliveryMode: "OnSite",
                duration: 60,
                courseId: "",
            });
        }
    }, [session]);

    const handleSubmit = (values: typeof form.values) => {
        if(session) {
            onUpdate({
                ...values,
                deliveryMode: values.deliveryMode == "OnSite" ? DeliveryMode.OnSite : DeliveryMode.Remote,
                startDate: new Date(values.startDate),
            });
        }
        else
        {
            //TODO handle create
        }
        onClose();
    };

    return (
        <Modal opened={opened} onClose={onClose}
               title={session ? "Éditer la session" : "Ajouter une session"}
               centered size="lg">
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <DateTimePicker label="Date de début" valueFormat="DD/MM/YYYY" {...form.getInputProps("startDate")} mb="sm"/>

                <Select
                    label="Mode de formation"
                    data={[
                        {value: "OnSite", label: "Présentiel"},
                        {value: "Remote", label: "À distance"},
                    ]}
                    {...form.getInputProps("deliveryMode")}
                    mb="sm"
                />

                <NumberInput
                    label="Durée (minutes)"
                    {...form.getInputProps("duration")}
                    min={1}
                    mb="sm"
                />

                <Select
                    label="Formation"
                    data={list.map((d) => ({value: d.id, label: d.name}))}
                    {...form.getInputProps("courseId")}
                    mb="sm"
                    renderOption={(item) => {
                        const course = courses[item.option.value];
                        return course ? <CourseSelectItem course={course}/> : null;
                    }}
                />

                <Group mt="md">
                    <Button variant="outline" onClick={onClose}>
                        Annuler
                    </Button>
                    <Button type="submit">
                        {session ? "Sauvegarder" : "Ajouter"}
                    </Button>
                </Group>
            </form>
        </Modal>
    );
};

export default SessionAddEditModal;