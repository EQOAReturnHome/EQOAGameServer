import Generate from "../comp/Generate";
import { useState } from "react";

import { Button, Form, Input, Select, Space } from "antd";
import React from "react";

const race = [
    {
        label: "Human",
        value: "human",
    },
    {
        label: "Elf",
        value: "elf",
    },
    {
        label: "Dark Elf",
        value: "dark_elf",
    },
    {
        label: "Gnome",
        value: "gnome",
    },
    {
        label: "Dwarf",
        value: "dwarf",
    },
    {
        label: "Troll",
        value: "troll",
    },
    {
        label: "Barbarian",
        value: "barbarian",
    },
    {
        label: "Halfling",
        value: "halfling",
    },
    {
        label: "Erudite",
        value: "erudite",
    },
    {
        label: "Ogre",
        value: "ogre",
    },
];

const Quests = () => {
    const [form] = Form.useForm();

    const onFinish = (values) => {
        console.log({ values });
    };

    const handleChange = () => {
        const [finalStatus, setStatus] = useState();
        setStatus(form.getFieldValue("quest_status"));

        form.setFieldsValue({
            sights: [],
        });
    };

    return (
        <div>
            <Form
                form={form}
                name="dynamic_form_nest_item"
                onFinish={onFinish}
                autoComplete="off"
            >
                <Form.Item
                    name="npc_name"
                    label="NPC Name"
                    rules={[
                        {
                            required: true,
                            message: "Missing status",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
	    	    <input type="text" id="name" name="name"/>
                </Form.Item>
	    <button onclick="">Search</button>
                <Form.Item
                    name="x_coord"
                    label="X Coordinate"
                    rules={[
                        {
                            required: true,
                            message: "Missing area",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item
                    name="y_coord"
                    label="Y Coordinate"
                    rules={[
                        {
                            required: true,
                            message: "Missing area",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select options={race} />
                </Form.Item>
                <Form.Item
                    name="z_coord"
                    label="Z Coordinate"
                    rules={[
                        {
                            required: true,
                            message: "Missing type",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item
                    name="facing"
                    label="Facing Coordinate"
                    rules={[
                        {
                            required: true,
                            message: "Missing type",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item
                    name="world"
                    label="World Designation"
                    rules={[
                        {
                            required: true,
                            message: "Missing type",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select/>
                </Form.Item>
		                <Form.Item
                    name="hp"
                    label="Max HP"
                    rules={[
                        {
                            required: true,
                            message: "Missing type",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item
                    name="modelid"
                    label="NPC Model"
                    rules={[
                        {
                            required: true,
                            message: "Missing type",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item>
                    {/* <Generate finalStatus=$finalStatus /> */}
                </Form.Item>
            </Form>
        </div>
    );
};
export default Quests;
