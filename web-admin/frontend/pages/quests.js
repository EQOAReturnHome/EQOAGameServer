import Generate from "../comp/Generate";
import { useState } from "react";

import { Button, Form, Input, Select, Space } from "antd";
import React from "react";

const npc = [
    {
        label: "A Dusty Tome",
        value: "a_dusty_tome",
    },
    {
        label: "Aeric Sparelli",
        value: "aeric_sparelli",
    },
    {
        label: "Agead",
        value: "agead",
    },
];

const classes = [
    {
        label: "Warrior",
        value: "warrior",
    },
    {
        label: "Ranger",
        value: "ranger",
    },
    {
        label: "Paladin",
        value: "paladin",
    },
    {
        label: "Shadowknight",
        value: "shadowknight",
    },
    {
        label: "Monk",
        value: "monk",
    },
    {
        label: "Bard",
        value: "bard",
    },
    {
        label: "Rogue",
        value: "rogue",
    },
    {
        label: "Druid",
        value: "druid",
    },
    {
        label: "Shaman",
        value: "shaman",
    },
    {
        label: "Cleric",
        value: "cleric",
    },
    {
        label: "Magician",
        value: "magician",
    },
    {
        label: "Necromancer",
        value: "necromancer",
    },
    {
        label: "Enchanter",
        value: "enchanter",
    },
    {
        label: "Wizard",
        value: "wizard",
    },
    {
        label: "Alchemist",
        value: "alchemist",
    },
];

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

const type = [
    {
        label: "Other",
        value: "other",
    },
    {
        label: "Eastern",
        value: "eastern",
    },
    {
        label: "Western",
        value: "western",
    },
];

const status = [
    {
        label: "Starting",
        value: "starting",
    },
    {
        label: "Continuing",
        value: "continuing",
    },
    {
        label: "Completing",
        value: "completing",
    },
];

const locations = [
    {
        label: "Bobble By Water",
        value: "bobble_by_water",
    },
    {
        label: "Darvar Manor",
        value: "darvar_manor",
    },
    {
        label: "Fayspires/Teth",
        value: "fayspires_teth",
    },
    {
        label: "Freeport",
        value: "freeport",
    },
    {
        label: "Fort Seriak",
        value: "ft_seriak",
    },
    {
        label: "Grobb",
        value: "grobb",
    },
    {
        label: "Halas",
        value: "halas",
    },
    {
        label: "Highbourne",
        value: "highbourne",
    },
    {
        label: "Highpass",
        value: "highpass",
    },
    {
        label: "Klick Anon",
        value: "klick_anon",
    },
    {
        label: "Mordahim",
        value: "mordahim",
    },
    {
        label: "Muniel's Tea Garden",
        value: "muniels_tea_garden",
    },
    {
        label: "Neriak",
        value: "neriak",
    },
    {
        label: "Oasis of Marr",
        value: "oasis_of_marr",
    },
    {
        label: "Oggok",
        value: "oggok",
    },
    {
        label: "Qeynos",
        value: "qeynos",
    },
    {
        label: "Rivervale",
        value: "rivervale",
    },
    {
        label: "South Crossroads",
        value: "south_crossroads",
    },
    {
        label: "Surefall Glade",
        value: "surefall_glade",
    },
    {
        label: "Wyndhaven",
        value: "wyndhaven",
    },
];

const Quests = () => {
    const [form] = Form.useForm();

    const onFinish = (values) => {
        // bingo
        // var finalStatus = form.getFieldValue("quest_status");
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
                    name="quest_status"
                    label="Quest Status"
                    rules={[
                        {
                            required: true,
                            message: "Missing status",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select options={status} />
                </Form.Item>
                <Form.Item
                    name="location"
                    label="Location"
                    rules={[
                        {
                            required: true,
                            message: "Missing location",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select options={locations} />
                </Form.Item>
                <Form.Item
                    name="npc"
                    label="NPC Name"
                    rules={[
                        {
                            required: true,
                            message: "Missing type",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select options={npc} />
                </Form.Item>
                <Form.Item
                    name="class"
                    label="Class"
                    rules={[
                        {
                            required: true,
                            message: "Missing area",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select options={classes} />
                </Form.Item>
                <Form.Item
                    name="race"
                    label="Race"
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
                    name="type"
                    label="Type"
                    rules={[
                        {
                            required: true,
                            message: "Missing type",
                        },
                    ]}
                    onValuesChange={handleChange}
                >
                    <Select options={type} />
                </Form.Item>
                <Form.Item>
                    {/* <Generate finalStatus=$finalStatus /> */}
                </Form.Item>
            </Form>
        </div>
    );
};

export default Quests;
