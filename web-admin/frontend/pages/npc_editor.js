import Generate from "../comp/Generate";
import { useState } from "react";

import { Button, Form, Input, Select, Space } from "antd";
import React from "react";
import {characterClass, characterRace} from "./npc_arrays";

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


 async function postData(url = '', data = {}) {
    
    const response = await fetch(url, {
        method: 'POST',
	headers: {
		'Accept': 'application/json',
		'Content-Type': 'application/json'
	},
	
        body: JSON.stringify({"npc_name":data})
    });
    
    const json = await response.json();
    alert(JSON.stringify(json))
    document.getElementById("x_coord").value = json.x_coord;
    document.getElementById("y_coord").value = json.y_coord;
    document.getElementById("z_coord").value = json.z_coord;
    document.getElementById("facing").value = json.facing;
    document.getElementById("world").value = json.world;
    return json
 }
      
  async function getresponse (){
    var data = document.getElementById("name").value;
    postData('http://192.168.6.100:8000/get_npc', data)
   }

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
                            required: false,
                            message: "Missing status",
                        },
                    ]}
                >
	    	    <input type="text" id="name" name="name"/>
		    <button onClick={getresponse}>Search</button>
                </Form.Item>
                <Form.Item
                    name="x_coord"
                    label="X Coordinate"
                    rules={[
                        {
                            required: false,
                            message: "Missing area",
                        },
                    ]}
                >
		    <input type="text" id="x_coord" name="x_coord"/>
                </Form.Item>
                <Form.Item
                    name="y_coord"
                    label="Y Coordinate"
                    rules={[
                        {
                            required: false,
                            message: "Missing area",
                        },
                    ]}
                    onChange={handleChange}
                >
                    <Select options={race} />
                </Form.Item>
                <Form.Item
                    name="z_coord"
                    label="Z Coordinate"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                    onChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item
                    name="facing"
                    label="Facing Coordinate"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                    onChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item
                    name="world"
                    label="World Designation"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                    onChange={handleChange}
                >
                    <Select/>
                </Form.Item>
		                <Form.Item
                    name="hp"
                    label="Max HP"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                    onChange={handleChange}
                >
                    <Select/>
                </Form.Item>
                <Form.Item
                    name="modelid"
                    label="NPC Model"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                    onChange={handleChange}
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
