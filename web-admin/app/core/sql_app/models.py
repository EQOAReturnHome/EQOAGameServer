from sqlalchemy import Column, Integer, String, Float
from app.core.sql_app.database import Base


class NPCInfo(Base):
    __tablename__ = "npcs"

    id = Column(Integer, unique=True, primary_key=True, index=True)
    npc_name = Column(String, unique=True)
    x_coord = Column(Float)
    y_coord = Column(Float)
    z_coord = Column(Float)
    facing = Column(Float)
    world = Column(Integer)
    hp = Column(Integer)
    modelid = Column(Integer)
    size = Column(Integer)
    primary_weapon = Column(Integer)
    secondary_weapon = Column(Integer)
    shield = Column(Integer)
    torso = Column(Integer)
    forearm = Column(Integer)
    gloves = Column(Integer)
    legs = Column(Integer)
    feet = Column(Integer)
    head = Column(Integer)
    hair_color = Column(Integer)
    hair_length = Column(Integer)
    hair_style = Column(Integer)
    face = Column(Integer)
    robe_style = Column(String)
    race = Column(String)
    npc_level = Column(Integer)
    npc_type = Column(Integer)
    npc_id = Column(Integer)

class ItemInfo(Base):
    __tablename__ = "itempattern"

    patternid = Column(Integer, unique=True, primary_key=True, index=True)
    patternfam = Column(Integer)
    itemicon = Column(Integer)
    equipslot = Column(Integer)
    trade = Column(Integer)
    rent = Column(Integer)
    attacktype = Column(Integer)
    weapondamage = Column(Integer)
    levelreq = Column(Integer)
    maxstack = Column(Integer)
    maxhp = Column(Integer)
    duration = Column(Integer)
    classuse = Column(Integer)
    raceuse = Column(Integer)
    procanim = Column(Integer)
    lore = Column(Integer)
    craft = Column(Integer)
    itemname = Column(String)
    itemdesc = Column(String)
    model = Column(Integer)
    color = Column(Integer)
    str = Column(Integer)
    sta = Column(Integer)
    agi = Column(Integer)
    wis = Column(Integer)
    dex = Column(Integer)
    cha = Column(Integer)
    intelligence = Column(Integer)
    HPMAX = Column(Integer)
    POWMAX = Column(Integer)
    PoT = Column(Integer)
    HoT = Column(Integer)
    AC = Column(Integer)
    PR = Column(Integer)
    DR = Column(Integer)
    FR = Column(Integer)
    CR = Column(Integer)
    LR = Column(Integer)
    AR = Column(Integer)
    weaponproc = Column(Integer)
    fish = Column(Integer)
