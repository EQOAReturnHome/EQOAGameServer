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
    itemname = Column(String)
    patternfam = Column(Integer, default=0)
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
    classuse = Column(Integer, default = 32767)
    raceuse = Column(Integer, default = 1023)
    procanim = 0
    lore = 0
    craft = 0
    itemdesc = Column(String, default="This is just placeholder text, please update.")
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
