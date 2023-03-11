import Generate from "../comp/Generate";
import { useState } from "react";
import Select from "react-select";
import { Button, Form, Input, Space } from "antd";
import React, { Component } from "react";
import { item_models, attack_types } from "./item_arrays";
import { characterRace, characterClass } from "./npc_arrays";
import { icon_models } from "./icon_arrays";

const backend_url = "http://eqoa-admin.com:8000"

const Items = () => {
    const [form] = Form.useForm();
    const [selectedValue, setSelectedValue] = useState();

    const onFinish = (values) => {
        console.log({ values });
    };

    const handleChange = (e) => {
        setSelectedValue(e.value);
	
    };

    const handleChangedImage = (e) => {
        setSelectedValue(e.value);
    };


    async function addItem() {
        var itemname = document.getElementById("itemname").value;
        const url = backend_url + "/create_item";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },

            body: JSON.stringify({ itemname: itemname }),
        });

        const json = await response.json();
        alert(JSON.stringify(json));
        return json;
        
    }

    async function clearFields(){
    document.getElementById("itemForm").reset();
    }



    async function updateItem() {
        const url = backend_url + "/update_item";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },

            body: JSON.stringify({ itemname: data }),
        });
    }

    async function convertItemMask(classMask, raceMask) {


        if ((characterClass.Warrior & classMask) == characterClass.Warrior) {
            document.getElementById("warrior").checked = true;
        }
        if ((characterClass.Ranger & classMask) == characterClass.Ranger) {
            document.getElementById("ranger").checked = true;
        }
        if ((characterClass.Paladin & classMask) == characterClass.Paladin) {
            document.getElementById("paladin").checked = true;
        }
        if ((characterClass.ShadowKnight & classMask) == characterClass.ShadowKnight) {
            document.getElementById("shadowknight").checked = true;
        }
        if ((characterClass.Monk & classMask) == characterClass.Monk) {
            document.getElementById("monk").checked = true;
        }
        if ((characterClass.Bard & classMask) == characterClass.Bard) {
            document.getElementById("bard").checked = true;
        }
        if ((characterClass.Rogue & classMask) == characterClass.Rogue) {
            document.getElementById("rogue").checked = true;
        }
        if ((characterClass.Druid & classMask) == characterClass.Druid) {
            document.getElementById("druid").checked = true;
        }
        if ((characterClass.Shaman & classMask) == characterClass.Shaman) {
            document.getElementById("shaman").checked = true;
        }
        if ((characterClass.Cleric & classMask) == characterClass.Cleric) {
            document.getElementById("cleric").checked = true;
        }
        if ((characterClass.Magician & classMask) == characterClass.Magician) {
            document.getElementById("magician").checked = true;
        }
        if ((characterClass.Necromancer & classMask) == characterClass.Necromancer) {
            document.getElementById("necromancer").checked = true;
        }
        if ((characterClass.Enchanter & classMask) == characterClass.Enchanter) {
            document.getElementById("enchanter").checked = true;
        }
        if ((characterClass.Wizard & classMask) == characterClass.Wizard) {
            document.getElementById("wizard").checked = true;
        }
        if ((characterClass.Alchemist & classMask) == characterClass.Alchemist) {
            document.getElementById("alchemist").checked = true;
        }

        if ((characterRace.Human & raceMask) == characterRace.Human) {
            document.getElementById("human").checked = true;
        }
        if ((characterRace.Elf & raceMask) == characterRace.Elf) {
            document.getElementById("elf").checked = true;
        }
        if ((characterRace.Dark_Elf & raceMask) == characterRace.Dark_Elf) {
            document.getElementById("darkelf").checked = true;
        }
        if ((characterRace.Gnome & raceMask) == characterRace.Gnome) {
            document.getElementById("gnome").checked = true;
        }
        if ((characterRace.Troll & raceMask) == characterRace.Troll) {
            document.getElementById("troll").checked = true;
        }
        if ((characterRace.Barbarian & raceMask) == characterRace.Barbarian) {
            document.getElementById("barbarian").checked = true;
        }
        if ((characterRace.Halfling & raceMask) == characterRace.Halfling) {
            document.getElementById("halfling").checked = true;
        }
        if ((characterRace.Erudite & raceMask) == characterRace.Erudite) {
            document.getElementById("erudite").checked = true;
        }
        if ((characterRace.Ogre & raceMask) == characterRace.Ogre) {
            document.getElementById("ogre").checked = true;
        }
        if ((characterRace.Dwarf & raceMask) == characterRace.Dwarf) {
            document.getElementById("dwarf").checked = true;
        }

    }

    async function decColorToHex(decimalNum) {
        let colorHex = (decimalNum).toString(16);
        let opacity = colorHex.slice(-2);
        if (opacity == "ff") {
            document.getElementById("color_opacity").checked = true;
        }
    }

    async function calculateClassMask() {
        var war = document.getElementById("warrior").checked; //0
        var rang = document.getElementById("ranger").checked; //1
        var pal = document.getElementById("paladin").checked; //2
        var sk = document.getElementById("shadowknight").checked; //3
        var mnk = document.getElementById("monk").checked; //4
        var brd = document.getElementById("bard").checked; //5
        var rge = document.getElementById("rogue").checked; //6
        var drd = document.getElementById("druid").checked; //7
        var shm = document.getElementById("shaman").checked; //8
        var clr = document.getElementById("cleric").checked; //9
        var mag = document.getElementById("magician").checked; //10
        var nec = document.getElementById("necromancer").checked; //11
        var enc = document.getElementById("enchanter").checked; //12
        var wiz = document.getElementById("wizard").checked; //13
        var alc = document.getElementById("alchemist").checked; //14
        var classValue = 0;

            if(war)
                classValue += 1;

            if(rang)
                classValue += 2;

            if(pal)
                classValue += 4;

            if(sk)
                classValue += 8;

            if(mnk)
                classValue += 16;

            if(brd)
                classValue += 32;

            if(rge)
                classValue += 64;

            if(drd)
                classValue += 128;

            if(shm)
                classValue += 256;

            if(clr)
                classValue += 512;

            if(mag)
                classValue += 1024;

            if(nec)
                classValue += 2048;

            if(enc)
                classValue += 4096;

            if(wiz)
                classValue += 8192;

            if(alc)
                classValue += 16384;
    }


    async function getColor(color) {
        console.log(document.getElementById("colorpicker").value);
    }

    async function postGetItem(url = "", data = {}) {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },

            body: JSON.stringify({ itemname: data }),
        });

        const json = await response.json();
        alert(JSON.stringify(json));
        document.getElementById("patternfam").value = json.patternfam;
	try {
        document.getElementById("itemicon").value = json.itemicon;
	} catch (err) {}
        document.getElementById("equipslot").value = json.equipslot;
        if (json.trade == 1) {
            document.getElementById("trade").checked = false;
        } else {
            document.getElementById("trade").checked = true;
        }
        if (json.rent == 1) {
            document.getElementById("rent").checked = false;
        } else {
            document.getElementById("rent").checked = true;
        }
        try {
            document.getElementById("attacktype").value = json.attacktype;
        } catch (err) { }
        document.getElementById("weapondamage").value = json.weapondamage;
        document.getElementById("levelreq").value = json.levelreq;
        document.getElementById("maxstack").value = json.maxstack;
        document.getElementById("maxhp").value = json.maxhp;
        document.getElementById("duration").value = json.duration;
        convertItemMask(json.classuse, json.raceuse);
        document.getElementById("procanim").value = json.procanim;
        if (json.lore == 0) {
            document.getElementById("lore").checked = false;
        } else {
            document.getElementById("lore").checked = true;
        }
        if (json.craft == 0) {
            document.getElementById("craft").checked = false;
        } else {
            document.getElementById("craft").checked = true;
        }
        document.getElementById("itemdesc").value = json.itemdesc;
        try {
	    let model_val = item_models.filter(function(p) { return p.model === json.model })
            document.getElementById("model").value = model_val[0].value;
	selectedValue = model_val[0].value;
        } catch (err) { }
        document.getElementById("colorpicker").value = decColorToHex(json.color);
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
	console.log(item_models.filter(function(p) { return p.model === json.model }));
        return json;
    }

    async function getItem() {
        var elements = document.getElementsByTagName("input");
        for (var ii = 0; ii < elements.length; ii++) {
            if (elements[ii].type == "checkbox") {
                elements[ii].checked = false;
            }
        }
        var data = document.getElementById("itemname").value;
        postGetItem(backend_url + "/get_item", data);
    }

    return (
        <div>
            <Form
                form={form}
                name="dynamic_form_nest_item"
                onFinish={onFinish}
                autoComplete="off"
		id="itemForm"
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
                    <input type="text" id="itemname" name="itemname" />
                    <button onClick={getItem}>Search</button>
                    <button onClick={addItem}>Add Item</button>
                    <button onClick={updateItem}>Update Item</button>
		    <button onClick={clearFields}>Clear Form</button>
                </Form.Item>
                <Form.Item
                    id="icon"
                    label="Icon"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <Select
                        placeholder="Select Option"
                        value={item_models.filter(
                            (obj) => obj.value === selectedValue
                        )} // set selected value
                        options={icon_models} // set list of the data
                        onChange={handleChange} // assign onChange function
                    />
                </Form.Item>
                <b>Selected Value (feel free to remove this): </b>{" "}
                {selectedValue}
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
                    <input type="text" id="equipslot" name="eqipslot" />
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
                    <input type="checkbox" id="trade" name="trade" />
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
                    <input type="checkbox" id="rent" name="rent" />
                </Form.Item>
                <Form.Item
                    id="atk_types"
                    label="Attack Type"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <Select
                        placeholder="Select Option" id="attack_type"
                        options={attack_types} // set list of the data
                    />
                    <b>Selected Value (feel free to remove this): </b>{" "}
                    {selectedValue}
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
                    <input type="text" id="weapondamage" name="weapondamage" />
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
                    <input type="text" id="levelreq" name="levelreq" />
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
                    <input type="text" id="maxstack" name="maxstack" />
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
                    <input type="text" id="maxhp" name="maxhp" />
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
                    <input type="text" id="duration" name="duration" />
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
                    <td>
                        <input type="checkbox" id="warrior" name="warrior" />
                        <label for="warrior"> Warrior </label>
                        <input type="checkbox" id="ranger" name="ranger" />
                        <label for="ranger"> Ranger  </label>
                        <input type="checkbox" id="paladin" name="paladin" />
                        <label for="paladin"> Paladin </label>
                        <input type="checkbox" id="shadowknight" name="shadowknight" />
                        <label for="shadowknight"> Shadowknight </label>
                        <input type="checkbox" id="monk" name="monk" />
                        <label for="monk"> Monk  </label>
                        <input type="checkbox" id="bard" name="bard" />
                        <label for="bard"> Bard  </label>
                        <input type="checkbox" id="rogue" name="rogue" />
                        <label for="rogue"> Rogue  </label>
                        <input type="checkbox" id="druid" name="druid" />
                        <label for="druid"> Druid  </label>
                        <input type="checkbox" id="shaman" name="shaman" />
                        <label for="shaman"> Shaman  </label>
                        <input type="checkbox" id="cleric" name="cleric" />
                        <label for="cleric"> Cleric  </label>
                        <input type="checkbox" id="magician" name="magician" />
                        <label for="magician"> Magician  </label>
                        <input type="checkbox" id="necromancer" name="necromancer" />
                        <label for="necromancer"> Necromancer  </label>
                        <input type="checkbox" id="enchanter" name="enchanter" />
                        <label for="enchanter"> Enchanter </label>
                        <input type="checkbox" id="wizard" name="wizard" />
                        <label for="wizard"> Wizard  </label>
                        <input type="checkbox" id="alchemist" name="alchemist" />
                        <label for="alchemist"> Alchemist  </label>
                    </td>
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
                        <input type="checkbox" id="human" name="human" />
                        <label for="human"> Human </label>
                        <input type="checkbox" id="elf" name="elf" />
                        <label for="elf"> Elf </label>
                        <input type="checkbox" id="darkelf" name="darkelf" />
                        <label for="darkelf"> Dark Elf </label>
                        <input type="checkbox" id="gnome" name="gnome" />
                        <label for="gnome"> Gnome </label>
                        <input type="checkbox" id="dwarf" name="dwarf" />
                        <label for="dwarf"> Dwarf </label>
                        <input type="checkbox" id="troll" name="troll" />
                        <label for="troll"> Troll </label>
                        <input type="checkbox" id="barbarian" name="barbarian" />
                        <label for="barbarian"> Barbarian </label>
                        <input type="checkbox" id="halfling" name="halfling" />
                        <label for="halfling"> Halfing </label>
                        <input type="checkbox" id="erudite" name="erudite" />
                        <label for="erudite"> Erudite </label>
                        <input type="checkbox" id="ogre" name="ogre" />
                        <label for="ogre"> Ogre </label>
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
                    <input type="text" id="procanim" name="procanim" />
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
                    <input type="checkbox" id="lore" name="lore" />
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
                    <input type="checkbox" id="craft" name="craft" />
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
                    <input type="text" id="itemdesc" name="itemdesx" />
                </Form.Item>
                <Form.Item
                    id="model"
                    label="Model"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <Select
                        placeholder="Select Option"
                        value={item_models.filter(
                            (obj) => obj.value === selectedValue
                        )} // set selected value
                        options={item_models} // set list of the data
                        onChange={handleChange} // assign onChange function
                    />
                </Form.Item>
                <b>Selected Value (feel free to remove this): </b>{" "}
                {selectedValue}
                <Form.Item
                    id="color"
                    label="Item Color:"
                    rules={[
                        {
                            required: false,
                            message: "Missing type",
                        },
                    ]}
                >
                    <input type="color" id="colorpicker"
                        onChange={getColor} />
                    <input type="checkbox" id="color_opacity" />
                    <label for="color_opacity">Transparent</label>
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
                    <input type="text" id="str" name="str" />
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
                    <input type="text" id="sta" name="sta" />
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
                    <input type="text" id="agi" name="agi" />
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
                    <input type="text" id="wis" name="wis" />
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
                    <input type="text" id="dex" name="dex" />
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
                    <input type="text" id="cha" name="cha" />
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
                    <input type="text" id="intelligence" name="intelligence" />
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
                    <input type="text" id="patternfam" name="patternfam" />
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
                    <input type="text" id="HPMAX" name="HPMAX" />
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
                    <input type="text" id="POWMAX" name="POWMAX" />
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
                    <input type="text" id="PoT" name="PoT" />
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
                    <input type="text" id="HoT" name="HoT" />
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
                    <input type="text" id="AC" name="AC" />
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
                    <input type="text" id="PR" name="PR" />
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
                    <input type="text" id="DR" name="DR" />
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
                    <input type="text" id="FR" name="FR" />
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
                    <input type="text" id="CR" name="CR" />
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
                    <input type="text" id="LR" name="LR" />
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
                    <input type="text" id="AR" name="AR" />
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
                    <input type="text" id="weaponproc" name="weaponproc" />
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
                    <input type="text" id="fish" name="fish" />
                </Form.Item>
            </Form>
        </div>
    );
};
export default Items;
