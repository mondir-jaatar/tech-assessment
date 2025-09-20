import {useForm} from "@mantine/form";
import {useLogin} from "./hooks/useLogin";
import {Alert, Button, Container, Group, Paper, TextInput, Title, Text} from "@mantine/core";
import {IconAlertCircle} from "@tabler/icons-react";

const AuthenticationPage = () => {
    const form = useForm({
        initialValues: {username: ""},
        validate: {
            username: (value) =>
                value.trim().length === 0 ? "Nom d’utilisateur requis" : null,
        },
    });

    const loginMutation = useLogin();

    const handleLogin = (values: { username: string }) => {
        loginMutation.mutate({Login: values.username}, {
            onSuccess: () => {
                window.location.href = "/admin/sessions";
            },
        });
    };

    return <Container size={420} my={40}>
        <Title fw={700}>
            Connexion
        </Title>
        <Text c="dimmed" size="sm" mt={5}>
            Entrez simplement votre nom d’utilisateur
        </Text>

        <Paper withBorder shadow="md" p={30} mt={30} radius="md">
            {loginMutation.isError && (
                <Alert
                    icon={<IconAlertCircle size={16}/>}
                    title="Erreur"
                    color="red"
                    mb="md"
                >
                    {(loginMutation.error as any)?.message ?? "Échec de la connexion"}
                </Alert>
            )}

            <form onSubmit={form.onSubmit(handleLogin)}>
                <TextInput
                    label="Nom d’utilisateur"
                    placeholder="Ex: formation"
                    {...form.getInputProps("username")}
                />

                <Group justify="center" mt="lg">
                    <Button
                        type="submit"
                        fullWidth
                        loading={loginMutation.isPending}
                    >
                        Se connecter
                    </Button>
                </Group>
            </form>
        </Paper>
    </Container>;
}

export default AuthenticationPage;