import ReactDOM, { Container } from "react-dom/client";
import AdminPage from "./Admin.page";
import App from "../../App";

const root = ReactDOM.createRoot(document.getElementById("react-app") as Container);
root.render(<App><AdminPage></AdminPage></App>);
