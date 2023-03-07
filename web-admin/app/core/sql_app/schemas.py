from typing import List, Optional
from pydantic import BaseModel


class NPCInfoBase(BaseModel):
    npc_name: str


class CreateNPC(NPCInfoBase):
    id: int
    x_coord: int


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


class Config:
    orm_mode = True


class ItemInfo(BaseModel):
    itemname: str
    patternid: int
    patternfam: int
    itemicon = 0
    equipslot = 0
    trade = 0
    rent = 0
    attacktype = 0
    weapondamage = 0
    levelreq = 1
    maxstack = 1
    maxhp = 100
    duration = 50
    classuse = 32767
    raceuse = 1023
    procanim = 0
    lore = 0
    craft = 0
    itemdesc = 'This is just placeholder text, please update.'
    model = 0
    color = 0
    str = 0
    sta = 0
    agi = 0
    wis = 0
    dex = 0
    cha = 0
    intelligence = 0
    HPMAX = 0
    POWMAX = 0
    PoT = 0
    HoT = 0
    AC = 0
    PR = 0
    DR = 0
    FR = 0
    CR = 0
    LR = 0
    AR = 0
    weaponproc = 0
    fish = 0

    class Config:
        orm_mode = True


class GetItem(ItemInfoBase):
    itemname: str

class UpdateItem(ItemInfoBase):
    patternfam: Optional[int] = 0
    itemicon: Optional[int] = 0
    equipslot: Optional[int] = 0
    trade: Optional[int] = 0
    rent:  Optional[int] = 0
    attacktype: Optional[int] = 0
    weapondamage: Optional[int] = 0
    levelreq: Optional[int] = 1 
    maxstack: Optional[int] = 1 
    maxhp: Optional[int] = 100
    duration: Optional[int] = 50
    classuse: Optional[int] = 32767
    raceuse: Optional[int] = 1023
    procanim: Optional[int] = 0
    lore: Optional[int] = 0
    craft: Optional[int] = 0
    itemdesc: Optional[str] = 'This is just placeholder text, please update.'
    model: Optional[int] = 0
    color: Optional[int] = 0
    str: Optional[int] = 0
    sta: Optional[int] = 0
    agi: Optional[int] = 0
    wis: Optional[int] = 0
    dex: Optional[int] = 0
    cha: Optional[int] = 0
    intelligence: Optional[int] = 0
    HPMAX: Optional[int] = 0
    POWMAX: Optional[int] = 0
    PoT: Optional[int] = 0
    HoT: Optional[int] = 0
    AC: Optional[int] = 0
    PR: Optional[int] = 0
    DR: Optional[int] = 0
    FR: Optional[int] = 0
    CR: Optional[int] = 0
    LR: Optional[int] = 0
    AR: Optional[int] = 0
    weaponproc: Optional[int] = 0
    fish: Optional[int] = 0

    class Config:
        orm_mode = True


class CreateItem(ItemInfo):
    itemname: str
    patternfam = 0
    itemicon = 0
    equipslot = 0
    trade = 0
    rent = 0
    attacktype = 0
    weapondamage = 0
    levelreq = 1
    maxstack = 1
    maxhp = 100
    duration = 50
    classuse = 32767
    raceuse = 1023
    procanim = 0
    lore = 0
    craft = 0
    itemdesc = 'This is just placeholder text, please update.'
    model = 0
    color = 0
    str = 0
    sta = 0
    agi = 0
    wis = 0
    dex = 0
    cha = 0
    intelligence = 0
    HPMAX = 0
    POWMAX = 0
    PoT = 0
    HoT = 0
    AC = 0
    PR = 0
    DR = 0
    FR = 0
    CR = 0
    LR = 0
    AR = 0
    weaponproc = 0
    fish = 0

    class Config:
        orm_mode = True
        validate_assignment = True
