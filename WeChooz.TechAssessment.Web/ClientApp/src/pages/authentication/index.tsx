import ReactDOM, { Container } from "react-dom/client";
import App from "../../App";
import AuthenticationPage from "./Authentication.page.tsx";

const root = ReactDOM.createRoot(document.getElementById("react-app") as Container);
root.render(<AuthenticationPage></AuthenticationPage>);
