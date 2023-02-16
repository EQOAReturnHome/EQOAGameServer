from typing import List
from pydantic import BaseModel


class NPCInfoBase(BaseModel):
    npc_name: str

class CreateNPC(NPCInfoBase):
    id: int

class NPCInfo(NPCInfoBase):
    id: int
    x_coord: int
    y_coord: int
    z_coord: int
    facing: int
    world: int
    hp: int
    modelid: int
    size: int
    primary_weapon: int
    secondary_weapon: int
    shield: int
    torso: int
    forearm: int
    gloves: int
    legs: int
    feet: int
    head: int
    hair_color: int
    hair_length: int
    hair_style: int
    face: int
    robe_style: str
    race: str
    npc_level: int
    npc_type: int
    npc_id: int
    pd_icon: int

    class Config:
        orm_mode = True

class ItemInfoBase(BaseModel):
    itemname: str

class CreateItem(ItemInfoBase):
    itemname: str

class ItemInfo(ItemInfoBase):
    patternid: int
    patternfam: int
    itemicon: int
    equipslot: int
    trade: int
    rent: int
    attacktype: int
    weapondamage: int
    levelreq: int
    maxstack: int
    maxhp:int
    duration:int
    classuse: int
    raceuse: int
    procanim: int
    lore: int
    craft: int
    itemname: str
    itemdesc: str
    model: int
    color: int
    str: int
    sta: int
    agi: int
    wis: int
    dex: int
    cha: int
    intelligence: int
    HPMAX: int
    POWMAX: int
    PoT: int
    HoT: int
    AC: int
    PR: int
    DR: int
    FR: int
    CR: int
    LR: int
    AR: int
    weaponproc: int
    fish: int

    class Config:
        orm_mode = True
