import {useDisclosure} from "@mantine/hooks";
import {Burger, Group, Text} from "@mantine/core";

const Header = () => {
    const [opened, { toggle }] = useDisclosure(false);

    return (
        <Group h="100%" px="md">
            <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
            <Text fw={1000} fz="xl">Catalogue formation</Text>
        </Group>
    );
}

export default Header;