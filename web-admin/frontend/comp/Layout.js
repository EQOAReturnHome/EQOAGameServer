import {
    HomeOutlined,
    BuildOutlined,
    DesktopOutlined,
    ApiOutlined,
    GithubOutlined,
    LineChartOutlined,
} from "@ant-design/icons";
import { PageHeader, Layout, Menu } from "antd";
import React, { useState } from "react";
const { Header, Content, Footer, Sider } = Layout;
import Link from "next/link";

function getItem(label, key, icon, children) {
    return {
        key,
        icon,
        children,
        label,
    };
}

const items = [
    getItem(<Link href="/">Home</Link>, "1", <HomeOutlined />),
    getItem(<Link href="/quests">Quest Builder</Link>, "2", <BuildOutlined />),
    getItem(<Link href="#">RH Server APIs</Link>, "3", <DesktopOutlined />),
    getItem(<Link href="/docs">Web API Docs</Link>, "4", <ApiOutlined />),
    getItem(
        <Link href="https://github.com/EQOAReturnHome/EQOAGameServer">
            Github Project
        </Link>,
        "5",
        <GithubOutlined />
    ),
    getItem(<Link href="#">Docker Logs</Link>, "6", <LineChartOutlined />),
];

const MainLayout = ({ children }) => {
    const [collapsed, setCollapsed] = useState(false);
    return (
        <Layout
            style={{
                minHeight: "100vh",
            }}
        >
            <Sider
                collapsible
                collapsed={collapsed}
                onCollapse={(value) => setCollapsed(value)}
            >
                <div className="logo" />
                <Menu
                    theme="dark"
                    defaultSelectedKeys={["1"]}
                    mode="inline"
                    items={items}
                />
            </Sider>
            <Layout className="site-layout">
                <PageHeader
                    style={{
                        textAlign: "center",
                        fontWeight: "bold",
                        fontSize: "large",
                    }}
                >
                    Web Admin - Project Return Home
                </PageHeader>
                <Content
                    style={{
                        margin: "0 16px",
                    }}
                >
                    <div>{children}</div>
                </Content>
                {/* <Footer
                    style={{
                        textAlign: "center",
                    }}
                >
                    PRH
                </Footer> */}
            </Layout>
        </Layout>
    );
};

export default MainLayout;
