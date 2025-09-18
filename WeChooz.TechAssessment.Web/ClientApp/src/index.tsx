import ReactDOM, { Container } from "react-dom/client";
import App from "./App";
import PublicPage from "./pages/Public.page.tsx";

const root = ReactDOM.createRoot(document.getElementById("react-app") as Container);
root.render(<App><PublicPage></PublicPage></App>);
