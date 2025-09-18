import {
    Text, Card, Group, Badge, Button, SegmentedControl, Container, rem, SimpleGrid,
    Paper
} from "@mantine/core";
import {IconCalendar, IconClock, IconHome, IconMapPin, IconUser, IconUsers} from "@tabler/icons-react";
import {DatePickerInput} from "@mantine/dates";
import {useEffect, useState} from "react";
import {SessionService} from "../../api/session/session-service.tsx";
import {GetSessionsFromPublicListingPageViewModelResponse} from "../../api/session/queries/get-sessions-from-public-listing-page-query.tsx";


const PublicPage = () => {
    const [targetAudience, setTargetAudience] = useState('all');
    const [deliveryMode, setDeliveryMode] = useState('all');
    const [startDate, setStartDate] = useState(undefined);
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

    const sessions = [
        {
            name: "Introduction à React",
            description: "Apprenez les bases de React et construisez votre première application web moderne.",
            targetAudience: "Débutants",
            startDate: "2025-10-20",
            duration: "3 jours",
            deliveryMode: "Présentiel",
            remainingSeats: 5,
            trainer: "Jane Doe"
        },
        {
            name: "Maîtriser Node.js",
            description: "Plongez dans le monde du développement back-end avec Node.js et Express.",
            targetAudience: "Intermédiaires",
            startDate: "2025-11-05",
            duration: "5 jours",
            deliveryMode: "À distance",
            remainingSeats: 2,
            trainer: "John Smith"
        },
        {
            name: "Développement Mantine UI",
            description: "Apprenez à construire des interfaces utilisateur élégantes et réactives avec Mantine.",
            targetAudience: "Débutants",
            startDate: "2025-11-15",
            duration: "2 jours",
            deliveryMode: "À distance",
            remainingSeats: 10,
            trainer: "Alex Johnson"
        },
        {
            name: "Bases de données SQL",
            description: "Comprenez les concepts fondamentaux des bases de données relationnelles et du langage SQL.",
            targetAudience: "Tous niveaux",
            startDate: "2025-12-01",
            duration: "4 jours",
            deliveryMode: "Présentiel",
            remainingSeats: 0, // Session complète
            trainer: "Sarah Miller"
        }
    ];

    useEffect(() => {
        SessionService.GetFromPublicListingPage({
            DeliveryMode: getDeliveryMode(deliveryMode),
            TargetAudience:getTargetAudience(targetAudience),
            PageNumber:1,
            PageSize: 100,
            StartDate: startDate,
            EndDate: startDate
        })
            .then((res: GetSessionsFromPublicListingPageViewModelResponse) => {
                // if (res.succeeded) {
                //     setResponse(res);
                // } else {
                //     toast.error(t('common.toasterMessages.errors.default'));
                // }
                console.log(res);
            });
    }, []);

    const cardItems = sessions.map((session, index) => {
        const isFull = session.remainingSeats === 0;
        const deliveryIcon = session.deliveryMode === 'Présentiel' ?
            <IconMapPin style={{width: rem(16), height: rem(16)}}/> :
            <IconHome style={{width: rem(16), height: rem(16)}}/>;

        return (
            <Card key={index} shadow="sm" padding="lg" radius="md" withBorder miw={400}>
                <Group justify="space-between" mb="xs">
                    <Text fw={500} size="lg">{session.name}</Text>
                    <Badge color={isFull ? 'red' : 'green'} variant="light">
                        {isFull ? 'Complet' : `${session.remainingSeats} places restantes`}
                    </Badge>
                </Group>

                <Text size="sm" c="dimmed" lineClamp={3}>
                    {session.description}
                </Text>

                <Group mt="md" gap="sm" wrap="nowrap">
                    <Badge variant="light" leftSection={<IconUsers style={{width: rem(14), height: rem(14)}}/>}>
                        {session.targetAudience}
                    </Badge>
                    <Badge variant="light" leftSection={deliveryIcon}>
                        {session.deliveryMode}
                    </Badge>
                </Group>

                <Group mt="sm" gap="sm" wrap="nowrap">
                    <Badge variant="outline" leftSection={<IconCalendar style={{width: rem(14), height: rem(14)}}/>}>
                        {session.startDate}
                    </Badge>
                    <Badge variant="outline" leftSection={<IconClock style={{width: rem(14), height: rem(14)}}/>}>
                        {session.duration}
                    </Badge>
                    <Badge variant="outline" leftSection={<IconUser style={{width: rem(14), height: rem(14)}}/>}>
                        {session.trainer}
                    </Badge>
                </Group>
            </Card>
        );
    });

    const handleFilter = () => {

    };

    return (
        <Container size="xl" py="xl">
            <Text fw={700} fz="xl" ta="center" mb={10}>Sessions de Formation</Text>

            <Paper shadow="sm" radius="md" p="md" mb="xl">
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
                            Date:
                        </Text>
                        <DatePickerInput
                            placeholder="Sélectionnez une date"
                            clearable
                            onClick={handleFilter}
                        />
                    </Group>

                    <Button variant="filled">Filtrer</Button>
                </Group>
            </Paper>

            <SimpleGrid cols={{base: 1, sm: 2, lg: 3}} spacing="md">
                {cardItems}
            </SimpleGrid>
        </Container>
    );
}

export default PublicPage;