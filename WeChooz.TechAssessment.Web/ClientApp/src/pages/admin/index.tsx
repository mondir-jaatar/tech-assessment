import ReactDOM, {Container} from "react-dom/client";
import App from "../../App";
import {Routes, Route, Navigate, BrowserRouter} from "react-router-dom";
import CoursesAdminPage from "./course/CoursesAdminPage.tsx";
import SessionsAdminPage from "./session/SessionsAdminPage.tsx";

const root = ReactDOM.createRoot(document.getElementById("react-app") as Container);
root.render(<App>
    <BrowserRouter>
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
            <Route path="*" element={<Navigate to="/admin" replace/>}/>
        </Routes>
    </BrowserRouter>
</App>);
