import {Container, Group, Text} from "@mantine/core";

const Footer = () => {
    return (
        <Container size="lg" h="100%">
            <Group justify="center" h="100%">
                <Text c="dimmed" size="sm">
                    © {new Date().getFullYear()} WeChooz. All rights reserved.
                </Text>
            </Group>
        </Container>
    );
}

export default Footer;