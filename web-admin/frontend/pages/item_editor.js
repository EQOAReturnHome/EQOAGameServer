import Generate from "../comp/Generate";
import { useState } from "react";
import ReactSelect from "react-select";
import { Button, Form, Input, Select, Space } from "antd";
import React, { Component } from "react";

const item_models = [
    { value: 'battleaxe', label: <div><img src="/item_models/battleaxe.jpg" height="200px" width="200px"/>Battleaxe</div> },
    { value: 'bearded_axe', label: <div><img src="/item_models/bearded_axe.jpg" height="200px" width="200px"/>Bearded Axe</div> },
  ];

const attack_types = [
    {
        label: "No Attack Type",
        value: "0",
    },
    {
        label: "1 Hand Slash",
        value: "1",
    },
    {
        label: "2 Hand Slash",
        value: "2",
    },
    {
        label: "1 Hand Blunt",
        value: "3",
    },
    {
        label: "2 Hand Blunt",
        value: "4",
    },
    {
        label: "1 Hand Pierce",
        value: "5",
    },
    {
        label: "2 Hand Pierce",
        value: "6",
    },
    {
        label: "Bow",
        value: "7",
    },
    {
        label: "1 Hand Crossbow",
        value: "8",
    },
    {
        label: "2 Hand Crossbow",
        value: "9",
    },
    {
        label: "Thrown",
        value: "10",
    }
]

const Items = () => {
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

async function update_image(){
	alert(document.getElementById("item-model").value);
}

async function display_attacktype(){
	var d = document.getElementById("attacktype");
	console.log(d.selectedIndex);
	//var d = document.getElementById("attacktype");
	//var atkvalue = d.options[d.selectedIndex].value;
	//let obj = attack_types.find(o => o.label === atkvalue);
	//alert(obj.value)

}

 async function calculateClassMask(){
	document.getElementById("ranger").checked
}

 async function postGetItem(url = '', data = {}) {
    
    const response = await fetch(url, {
        method: 'POST',
	headers: {
		'Accept': 'application/json',
		'Content-Type': 'application/json'
	},
	
        body: JSON.stringify({"itemname":data})
    });
    
    const json = await response.json();
    alert(JSON.stringify(json))
    document.getElementById("patternfam").value = json.patternfam;
    document.getElementById("itemicon").value = json.itemicon;
    document.getElementById("equipslot").value = json.equipslot;
    if (json.trade == 0){
    document.getElementById("trade").checked = false;
    } else {
    document.getElementById("trade").checked = true;
    }
    if (json.rent == 0){
    document.getElementById("rent").checked = false;
    } else {
    document.getElementById("rent").checked = true;
    }
    document.getElementById("attacktype").value = json.attacktype;
    document.getElementById("weapondamage").value = json.weapondamage;
    document.getElementById("levelreq").value = json.levelreq;
    document.getElementById("maxstack").value = json.maxstack;
    document.getElementById("maxhp").value = json.maxhp;
    document.getElementById("duration").value = json.duration;
    document.getElementById("classuse").value = json.classuse;
    document.getElementById("raceuse").value = json.raceuse;
    document.getElementById("procanim").value = json.procanim;
    if (json.lore == 0){
    document.getElementById("lore").checked = false;
    } else {
    document.getElementById("lore").checked = true;
    }
    if (json.craft == 0){
    document.getElementById("craft").checked = false;
    } else {
    document.getElementById("craft").checked = true;
    }
    document.getElementById("itemdesc").value = json.itemdesc;
    document.getElementById("model").value = json.model;
    document.getElementById("color").value = json.color;
    document.getElementById("str").value = json.str;
    document.getElementById("sta").value = json.sta;
    document.getElementById("agi").value = json.agi;
    document.getElementById("wis").value = json.wis;
    document.getElementById("dex").value = json.dex;
    document.getElementById("cha").value = json.cha;
    document.getElementById("intelligence").value = json.intelligence;
    document.getElementById("HPMAX").value = json.HPMAX;
    document.getElementById("POWMAX").value = json.POWMAX;
    document.getElementById("PoT").value = json.PoT;
    document.getElementById("HoT").value = json.HoT;
    document.getElementById("AC").value = json.AC;
    document.getElementById("PR").value = json.PR;
    document.getElementById("DR").value = json.DR;
    document.getElementById("FR").value = json.FR;
    document.getElementById("CR").value = json.CR;
    document.getElementById("LR").value = json.LR;
    document.getElementById("AR").value = json.AR;
    document.getElementById("weaponproc").value = json.weaponproc;
    document.getElementById("fish").value = json.fish;
    return json
 }
      
  async function getItem (){
    var data = document.getElementById("itemname").value;
    postGetItem('http://192.168.6.100:8000/get_item', data)
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
                    name="itemname"
                    label="Item Name"
                    rules={[
                        {
                            required: false,
                            message: "Missing status",
                        },
                    ]}
                >
	    	    <input type="text" id="itemname" name="itemname"/>
		    <button onClick={getItem}>Search</button>
		    <button onClick={getItem}>Add Item</button>
		    <button onClick={getItem}>Update Item</button>
                </Form.Item>
                <Form.Item
                    name="itemicon"
                    label="Item Icon"
                    rules={[
                        {
                            required: false,
                            message: "Missing area",
                        },
                    ]}
                >
		    <input type="text" id="itemicon" name="itemicon"/>
                </Form.Item>
                <Form.Item
                    name="equipslot"
                    label="Equip Slot"
                    rules={[
                        {
                            required: false,
                            message: "Missing area",
                        },
                    ]}
                >
		    <input type="text" id="equipslot" name="eqipslot"/>
                </Form.Item>
                <Form.Item
                    name="trade"
                    label="Trade"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
		    <input type="checkbox" id="trade" name="trade"/>
                </Form.Item>
                <Form.Item
                    name="rent"
                    label="Rent"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
		    <input type="checkbox" id="rent" name="rent"/>
                </Form.Item>
                <Form.Item
	            name="atk_types"
                    label="Attack Type"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
		    <Select options={attack_types} id="attacktype" name="attacktype" onChange={display_attacktype}/>
                </Form.Item>
		                <Form.Item
                    name="weapondamage"
                    label="Weapon Damage"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
		    <input type="text" id="weapondamage" name="weapondamage"/>
                </Form.Item>
                <Form.Item
                    name="levelreq"
                    label="Level Requirement"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
		    <input type="text" id="levelreq" name="levelreq"/>
                </Form.Item>
		<Form.Item
                    name="maxstack"
                    label="Max Stack"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="maxstack" name="maxstack"/>
                </Form.Item>
                <Form.Item
                    name="maxhp"
                    label="Item Max HP"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="maxhp" name="maxhp"/>
                </Form.Item>
                <Form.Item
                    name="duration"
                    label="Durability"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="duration" name="duration"/>
                </Form.Item>
                <Form.Item
                    name="classuse"
                    label="Class Use"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="classuse" name="classuse"/>
                </Form.Item>
                <Form.Item
                    name="raceuse"
                    label="Race Use"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
		<td>
                    <input type="checkbox" id="human" name="human"/>
		    <label for="human">  Human  </label>
                    <input type="checkbox" id="elf" name="elf"/>
		    <label for="elf">  Elf  </label>
		    <input type="checkbox" id="darkelf" name="darkelf"/>
                    <label for="darkelf">  Dark Elf  </label>
                    <input type="checkbox" id="gnome" name="gnome"/>
                    <label for="elf">  Gnome  </label>
		    <input type="checkbox" id="dwarf" name="dwarf"/>
                    <label for="human">  Dwarf  </label>
                    <input type="checkbox" id="troll" name="troll"/>
                    <label for="elf">  Troll </label>
		    <input type="checkbox" id="barbarian" name="barbarian"/>
                    <label for="human">  Barbarian </label>
                    <input type="checkbox" id="raceuse" name="raceuse"/>
                    <label for="elf">  Halfing  </label>
		    <input type="checkbox" id="human" name="human"/>
                    <label for="human">  Erudite  </label>
                    <input type="checkbox" id="raceuse" name="raceuse"/>
                    <label for="elf">  Ogre  </label>
                </td>
                </Form.Item>
                <Form.Item
                    name="procanim"
                    label="Proc Animation"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="procanim" name="procanim"/>
                </Form.Item>
                <Form.Item
                    name="lore"
                    label="Lore"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="checkbox" id="lore" name="lore"/>
                </Form.Item>
                <Form.Item
                    name="craft"
                    label="Craft"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="checkbox" id="craft" name="craft"/>
                </Form.Item>
                <Form.Item
                    name="itemdesc"
                    label="Item Description"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="itemdesc" name="itemdesx"/>
                </Form.Item>
                <Form.Item
                    name="model"
                    label="Model"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
		<Select id="item-model" options={item_models} onChange={update_image}/>
	   	<img id="item_img" src="https://gclabels.net/image/cache/data/new/inv/new/Blank-White-Square-Labels-s1w-600x600.png" height="200px" width="200px"/> 
                </Form.Item>
                <Form.Item
                    name="color"
                    label="Color"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="color" name="color"/>
                </Form.Item>
                <Form.Item
                    name="str"
                    label="Strength"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="str" name="str"/>
                </Form.Item>
                <Form.Item
                    name="sta"
                    label="Stamina"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="sta" name="sta"/>
                </Form.Item>
                <Form.Item
                    name="agi"
                    label="Agility"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="agi" name="agi"/>
                </Form.Item>
                <Form.Item
                    name="wis"
                    label="Wisdom"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="wis" name="wis"/>
                </Form.Item>
                <Form.Item
                    name="dex"
                    label="Dexterity"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="dex" name="dex"/>
                </Form.Item>
                <Form.Item
                    name="cha"
                    label="Charisma"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="cha" name="cha"/>
                </Form.Item>
                <Form.Item
                    name="intelligence"
                    label="Intelligence"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="intelligence" name="intelligence"/>
                </Form.Item>
                <Form.Item
                    name="patternfam"
                    label="Item Cost"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="patternfam" name="patternfam"/>
                </Form.Item>
                <Form.Item
                    name="HPMAX"
                    label="HP Max Increase"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="HPMAX" name="HPMAX"/>
                </Form.Item>
                <Form.Item
                    name="POWMAX"
                    label="Power Max Increase"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="POWMAX" name="POWMAX"/>
                </Form.Item>
                <Form.Item
                    name="PoT"
                    label="Power Over Time"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="PoT" name="PoT"/>
                </Form.Item>
                <Form.Item
                    name="HoT"
                    label="Heal Over Time"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="HoT" name="HoT"/>
                </Form.Item>
                <Form.Item
                    name="AC"
                    label="Armor Class"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="AC" name="AC"/>
                </Form.Item>
                <Form.Item
                    name="PR"
                    label="Poison Resistance"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="PR" name="PR"/>
                </Form.Item>
                <Form.Item
                    name="DR"
                    label="Disease Resistance"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="DR" name="DR"/>
                </Form.Item>
                <Form.Item
                    name="FR"
                    label="Fire Resistance"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="FR" name="FR"/>
                </Form.Item>
                <Form.Item
                    name="CR"
                    label="Cold Resistance"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="CR" name="CR"/>
                </Form.Item>
                 <Form.Item
                    name="LR"
                    label="Light Resistance"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="LR" name="LR"/>
                </Form.Item>
                <Form.Item
                    name="AR"
                    label="Arcane Resistance"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="AR" name="AR"/>
                </Form.Item>
                <Form.Item
                    name="weaponproc"
                    label="Weapon Proc"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="weaponproc" name="weaponproc"/>
                </Form.Item>
                <Form.Item
                    name="fish"
                    label="Fishing"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="text" id="fish" name="fish"/>
                </Form.Item>
            </Form>
        </div>
    );
};
export default Items;
