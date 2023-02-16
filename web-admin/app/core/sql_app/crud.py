
from sqlalchemy.orm import Session

from app.core.sql_app import models, schemas


def get_npc_by_name(db: Session, npc_name: str):
    return db.query(models.NPCInfo).filter(models.NPCInfo.npc_name == npc_name).first()

def create_npc(db: Session, npc: schemas.CreateNPC):
    new_npc = models.NPCInfo(npc_name = npc.npc_name)
    db.add(new_npc)
    db.commit()
    db.refresh(new_npc)
    return new_npc

def get_item_by_name(db: Session, itemname: str):
    return db.query(models.ItemInfo).filter(models.ItemInfo.itemname == itemname).first()

def create_item(db: Session, item: schemas.CreateItem):
    new_item = models.ItemInfo(itemname = item.itemname)
    db.add(new_item)
    db.commit()
    db.refresh(new_item)
    return new_item
