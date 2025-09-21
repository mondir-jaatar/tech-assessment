import ReactDOM, {Container} from "react-dom/client";
import App from "../../App";
import {Routes, Route, Navigate, BrowserRouter, Link} from "react-router-dom";
import CoursesAdminPage from "./course/CoursesAdminPage.tsx";
import SessionsAdminPage from "./session/SessionsAdminPage.tsx";
import {Group} from "@mantine/core";

const root = ReactDOM.createRoot(document.getElementById("react-app") as Container);
root.render(<App>
    <BrowserRouter>
        <Group>
            <Link to="/admin/sessions">Les sessions</Link>
            <Link to="/admin/courses">Les cours</Link>
        </Group>
        <Routes>
            <Route
                path="/admin/sessions"
                element={
                    <SessionsAdminPage/>
                }
            />
            <Route
                path="/admin/courses"
                element={
                    <CoursesAdminPage />
                }
            />
            <Route path="*" element={<Navigate to="/" replace/>}/>
        </Routes>
    </BrowserRouter>
</App>);
