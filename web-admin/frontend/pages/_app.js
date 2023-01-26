import "antd/dist/antd.css";
import MainLayout from "../comp/Layout";
import "../styles/globals.css";

function MyApp({ Component, pageProps }) {
    return (
        <MainLayout>
            <Component {...pageProps} />
        </MainLayout>
    );
}

export default MyApp;
