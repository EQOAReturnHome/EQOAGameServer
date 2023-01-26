import { useState } from "react";
import { Button } from "antd";
import SyntaxHighLighter from "react-syntax-highlighter";
import { dracula } from "react-syntax-highlighter/dist/cjs/styles/hljs";

const Generate = (props) => {
    const [data, setData] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [err, setErr] = useState("");

    const handleClick = async () => {
        setIsLoading(true);
        console.log("final status is: ");
        console.log(props.finalStatus);

        try {
            const response = await fetch(
                "http://localhost:8000/quests?status=" +
                    { status } +
                    "&location=" +
                    { location } +
                    "&npc=" +
                    { npc } +
                    "&race_type=" +
                    { race_type } +
                    "&step=" +
                    { step },
                {
                    method: "GET",
                    headers: {
                        Accept: "application/json",
                    },
                }
            );

            if (!response.ok) {
                throw new Error(`Error! status: ${response.status}`);
            }

            const result = await response.json();

            // console.log("result is: ", JSON.stringify(result, null, 4));

            setData(result);
        } catch (err) {
            setErr(err.message);
        } finally {
            setIsLoading(false);
        }
    };

    if (isLoading) return <p>Loading...</p>;
    if (!data)
        return (
            <div>
                {err && <h2>{err}</h2>}
                <Button
                    type="primary"
                    style={{
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                    }}
                    htmlType="submit"
                    onClick={handleClick}
                >
                    Generate Quest
                </Button>
                {isLoading && <h2>Loading...</h2>}
            </div>
        );
    // console.log(data.message);

    return (
        <div>
            {err && <h2>{err}</h2>}
            <Button
                style={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                }}
                type="primary"
                htmlType="submit"
                onClick={handleClick}
            >
                Generate Quest
            </Button>

            <h4>Output</h4>

            {isLoading && <h2>Loading...</h2>}

            <SyntaxHighLighter language="lua" style={dracula}>
                {data.message}
            </SyntaxHighLighter>
        </div>
    );
};

export default Generate;
