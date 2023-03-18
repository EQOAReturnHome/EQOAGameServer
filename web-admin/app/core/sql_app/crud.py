
from sqlalchemy.orm import Session
from app.core.sql_app.schemas import *
from app.core.sql_app import *


def get_npc_by_name(db: Session, npc_name: str):
    return db.query(models.NPCInfo).filter(models.NPCInfo.npc_name == npc_name).first()


def create_npc(db: Session, npc: schemas.CreateNPC):
    new_npc = models.NPCInfo(npc_name=npc.npc_name, x_coord=npc.x_coord)
    db.add(new_npc)
    db.commit()
    db.refresh(new_npc)
    return new_npc


def get_item_by_name(db: Session, itemname: str):
    return db.query(models.ItemInfo).filter(models.ItemInfo.itemname == itemname).first()


def create_item(db: Session, item: schemas.ItemInfo):
    new_item = models.ItemInfo(itemname=item.itemname, patternfam=item.patternfam, \
                               itemicon=item.itemicon, equipslot=item.equipslot, trade=item.trade, \
                                rent=item.rent, attacktype=item.attacktype, weapondamage=item.weapondamage, \
                                levelreq=item.levelreq, maxstack=item.maxstack, maxhp=item.maxhp, \
                                duration=item.duration, classuse=item.classuse, raceuse=item.raceuse, \
                                procanim=item.procanim, lore=item.lore, craft=item.craft, \
                                itemdesc=item.itemdesc, model=item.model, color=item.color, str=item.str, \
                                sta=item.sta, agi=item.agi, wis=item.wis, dex=item.dex, cha=item.cha, \
                                intelligence=item.intelligence, HPMAX=item.HPMAX, POWMAX=item.POWMAX, \
                                PoT=item.PoT, HoT=item.HoT, AC=item.AC, PR=item.PR, DR=item.DR, FR=item.FR, \
                                CR=item.CR, LR=item.LR, AR=item.AR, weaponproc=item.weaponproc, fish=item.fish)
    db.add(new_item)
    db.commit()
    db.refresh(new_item)
    return new_item

def update_item(db: Session, item: schemas.UpdateItem):
    old_item = db.query(models.ItemInfo).filter(models.ItemInfo.itemname == item.itemname).first()
    if old_item is None:
        return None

    for key,value in vars(item).items():
        setattr(old_item, key, value) if value else None
    
    db.add(old_item)
    db.commit()
    db.refresh(old_item)
    return old_item
