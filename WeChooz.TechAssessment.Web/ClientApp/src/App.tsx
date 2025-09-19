import {QueryClientProvider} from "@tanstack/react-query";
import {queryClient} from "./lib/queryClient.ts";
import React, {ReactNode, Suspense} from "react";
import {AppShell, Box, Container, Group, MantineProvider, Text} from '@mantine/core';
import {theme} from "./theme.ts";
import '@mantine/core/styles.css';
import '@mantine/dates/styles.css';
import Header from "./components/Header.tsx";
import Footer from "./components/Footer.tsx";

const App = ({children}: { children: ReactNode }) => {
    return (
        <MantineProvider theme={theme}>
            <QueryClientProvider client={queryClient}>
                <AppShell
                    header={{height: 60}}
                    footer={{height: 60}}
                    padding="md"
                >
                    <AppShell.Header>
                        <Header/>
                    </AppShell.Header>
                    <AppShell.Main>
                        {children}
                    </AppShell.Main>
                    <AppShell.Footer>
                        <Footer/>
                    </AppShell.Footer>
                </AppShell>
            </QueryClientProvider>
        </MantineProvider>
    );
};

export default App;