import {
    Text, Card, Group, Badge, Button, SegmentedControl, Container, rem, SimpleGrid,
    Paper, Center, Loader, Pagination, Modal, Tooltip, ActionIcon, Flex
} from "@mantine/core";
import {IconCalendar, IconClock, IconHome, IconInfoCircle, IconMapPin, IconUser, IconUsers} from "@tabler/icons-react";
import {DatePickerInput} from "@mantine/dates";
import {useEffect, useState} from "react";
import {SessionService} from "../../api/session/session-service.tsx";
import {CourseFromPublicListingPageDto, GetSessionsFromPublicListingPageQuery, GetSessionsFromPublicListingPageViewModelResponse, SessionFromPublicListingPageDto} from "../../api/session/queries/get-sessions-from-public-listing-page-query.tsx";
import {useQuery} from "@tanstack/react-query";
import {DeliveryMode} from "../../api/enums/delivery-mode.tsx";
import {TargetAudience} from "../../api/enums/target-audience.tsx";
import ReactMarkdown from "react-markdown";
import {ValidationErrorAsKeyValue} from "../../api/utilities/model/ValidationErrorAsKeyValue.tsx";


const PublicPage = () => {
    const [targetAudience, setTargetAudience] = useState('all');
    const [deliveryMode, setDeliveryMode] = useState('all');
    const [startDate, setStartDate] = useState<Date | undefined>();
    const [endDate, setEndDate] = useState<Date | undefined>();
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(9);
    const [opened, setOpened] = useState(false);
    const [selectedCourse, setSelectedCourse] = useState<CourseFromPublicListingPageDto | null>(null);
    const [query, setQuery] = useState<GetSessionsFromPublicListingPageQuery>({
        PageNumber: pageNumber,
        PageSize: pageSize,
    });

    const getDeliveryMode = (value: string) => {
        switch (value) {
            case 'Remote':
                return DeliveryMode.Remote;
            case 'OnSite':
                return DeliveryMode.OnSite;
            default:
                return undefined;
        }
    }

    const getTargetAudience = (value: string) => {
        switch (value) {
            case 'ElectedOfficial':
                return TargetAudience.ElectedOfficial;
            case 'WorksCouncilPresident':
                return TargetAudience.WorksCouncilPresident;
            default:
                return undefined;
        }
    }

    const {data, isLoading, isError, error} = useQuery({
        queryKey: ["publicListingPage", query],
        queryFn: () => SessionService.GetFromPublicListingPage(query)
    });

    const handleFilter = () => {
        setQuery((prev) => ({
            ...prev,
            PageNumber: 1,
            PageSize: pageSize,
            StartDate: startDate,
            EndDate: endDate,
            TargetAudience: getTargetAudience(targetAudience),
            DeliveryMode: getDeliveryMode(deliveryMode),
        }));
    };

    const handlePageChange = (page: number) => {
        setPageNumber(page);
        setQuery((prev) => ({
            ...prev,
            PageNumber: page,
            PageSize: pageSize,
        }));
    };

    const openModal = (description: any) => {
        setSelectedCourse(description);
        setOpened(true);
    };

    const closeModal = () => {
        setOpened(false);
        setSelectedCourse(null);
    };

    const SessionsList = () => {
        if (isError || !data?.data) {
            return <></>;
        }

        return data?.data.map((session: SessionFromPublicListingPageDto, index: number) => {
            const isFull = session.remainingSeats === 0;
            const deliveryIcon = session.deliveryMode == DeliveryMode.OnSite ?
                <IconMapPin style={{width: rem(16), height: rem(16)}}/> :
                <IconHome style={{width: rem(16), height: rem(16)}}/>;
            const formatDuration = (minutes: number): string => {
                if (minutes < 60) {
                    `${minutes} min`;
                }
                const hours = Math.floor(minutes / 60);
                const remainingMinutes = minutes % 60;
                return remainingMinutes > 0
                    ? `${hours}h ${remainingMinutes}min`
                    : `${hours}h`;
            }

            return (
                <Card key={index} shadow="sm" padding="lg" radius="md" withBorder miw={400}>
                    <Group justify="space-between" align="center" mb="xs">
                        <Flex align="center" gap="5px">
                            <Text fw={500} size="lg" display="inline">
                                {session.course.name}
                            </Text>

                            <Tooltip label="Voir les détails">
                                <ActionIcon
                                    size="sm"
                                    variant="subtle"
                                    onClick={() => openModal(session.course)}
                                >
                                    <IconInfoCircle size={16}/>
                                </ActionIcon>
                            </Tooltip>
                        </Flex>

                        <Badge color={isFull ? 'red' : 'green'} variant="light">
                            {isFull ? 'Complet' : `${session.remainingSeats} places restantes`}
                        </Badge>
                    </Group>

                    <Text size="sm" c="dimmed" lineClamp={3}>
                        {session.course.description.short}
                    </Text>

                    <Group mt="md" gap="sm" wrap="nowrap">
                        <Badge variant="light" leftSection={<IconUsers style={{width: rem(14), height: rem(14)}}/>}>
                            {session.course.targetAudience == TargetAudience.ElectedOfficial ? "Élu" : "Président de CS"}
                        </Badge>
                        <Badge variant="light" leftSection={deliveryIcon}>
                            {session.deliveryMode === DeliveryMode.OnSite ? "Présentiel" : "À distance"}
                        </Badge>
                    </Group>

                    <Group mt="sm" gap="sm" wrap="nowrap">
                        <Badge variant="outline" leftSection={<IconCalendar style={{width: rem(14), height: rem(14)}}/>}>
                            {new Date(session.startDate).toLocaleDateString("fr-FR")}
                        </Badge>
                        <Badge variant="outline" leftSection={<IconClock style={{width: rem(14), height: rem(14)}}/>}>
                            {formatDuration(session.duration)}
                        </Badge>
                        <Badge variant="outline" leftSection={<IconUser style={{width: rem(14), height: rem(14)}}/>}>
                            {session.trainer.firstName} {session.trainer.lastName.toUpperCase()}
                        </Badge>
                    </Group>
                </Card>
            );
        });
    }

    const getValidationMessage = (field: string): string | undefined => {
        if (!data || !data.IsValidationError || !data.Data)
        {
            return undefined;
        }

        const error = data.Data.find((e: ValidationErrorAsKeyValue) => e.Property === field);
        return error?.Message;
    }

    return (
        <>
            <Modal
                opened={opened}
                onClose={closeModal}
                title={selectedCourse?.name}
                size="lg"
            >
                <ReactMarkdown>
                    {selectedCourse?.description?.long || "Aucune description disponible."}
                </ReactMarkdown>
            </Modal>

            <Container size="xl" py="xl">
                <Text fw={700} fz="xl" ta="center" mb={10}>Sessions de Formation</Text>

                <Paper shadow="sm" radius="md" p="md" mb="xs">
                    <Group justify="space-between" align="center" gap="lg">
                        <Group gap="xs" style={{flexWrap: 'nowrap'}}>
                            <Text size="sm" c="dimmed">
                                Cible:
                            </Text>
                            <SegmentedControl
                                data={[
                                    {label: 'Tous', value: 'all'},
                                    {label: 'Élu', value: 'ElectedOfficial'},
                                    {label: 'Président de CSE', value: 'WorksCouncilPresident'},
                                ]}
                                defaultValue="all"
                                onChange={setTargetAudience}
                            />
                        </Group>

                        <Group gap="xs" style={{flexWrap: 'nowrap'}}>
                            <Text size="sm" c="dimmed">
                                Mode:
                            </Text>
                            <SegmentedControl
                                data={[
                                    {label: 'Tous', value: 'all'},
                                    {label: 'Présentiel', value: 'OnSite'},
                                    {label: 'À distance', value: 'Remote'},
                                ]}
                                defaultValue="all"
                                onChange={setDeliveryMode}
                            />
                        </Group>

                        <Group gap="xs">
                            <Text size="sm" c="dimmed">
                                De:
                            </Text>
                            <DatePickerInput
                                placeholder="Sélectionnez une date"
                                clearable
                                onChange={(value) => setStartDate(value ? new Date(value) : undefined)}
                                error={getValidationMessage("startDate")}
                            />
                        </Group>

                        <Group gap="xs">
                            <Text size="sm" c="dimmed">
                                à:
                            </Text>
                            <DatePickerInput
                                placeholder="Sélectionnez une date"
                                clearable
                                onChange={(value) => setEndDate(value ? new Date(value) : undefined)}
                                error={getValidationMessage("endDate")}
                            />
                        </Group>

                        <Button variant="filled" onClick={handleFilter}>Filtrer</Button>
                    </Group>
                </Paper>

                <Text fw={700} fz="md" ta="left" mb="xs">{data?.count ?? 0} sessions trouvé</Text>

                {
                    isLoading ?
                        <Center py="xl">
                            <Loader size="lg" variant="dots"/>
                        </Center>
                        :
                        (
                            <>
                                <SimpleGrid cols={{base: 1, sm: 2, lg: 3}} spacing="md">
                                    <SessionsList/>
                                </SimpleGrid>

                                <Center mt="xl">
                                    <Pagination
                                        total={data?.count / pageSize}
                                        value={pageNumber}
                                        onChange={handlePageChange}
                                    />
                                </Center>
                            </>
                        )
                }
            </Container>
        </>
    );
}

export default PublicPage;