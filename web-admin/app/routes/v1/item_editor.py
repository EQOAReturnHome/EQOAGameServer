from logging import Logger
from sqlalchemy.orm import Session
from typing import List, Optional
from fastapi import APIRouter, Depends, FastAPI, HTTPException
from app.core.sql_app import models, schemas, crud
from app.core.sql_app.database import engine, SessionLocal

log = Logger("logger")

router = APIRouter()

models.Base.metadata.create_all(bind=engine)

# Dependency

def get_db():
    db = None
    try:
        db = SessionLocal()
        yield db
    finally:
        db.close()

@router.post("/create_item", response_model=schemas.ItemInfo)
def create_item(item: schemas.CreateItem, db: Session = Depends(get_db)):
    new_item = crud.get_item_by_name(db, itemname=item.itemname)
    if new_item:
        raise HTTPException(status_code=400, detail="Item already exists")
    return crud.create_item(db=db, item=item)

@router.post("/get_item", response_model=schemas.ItemInfo)
def get_item(item: schemas.ItemInfoBase, db: Session = Depends(get_db)):
    new_item = crud.get_item_by_name(db, itemname=item.itemname)
    if new_item is None:
        raise HTTPException(status_code=400, detail="No item Found")
    return crud.get_item_by_name(db, itemname=item.itemname)

@router.get("/item_editor")
async def generate(
    player_class: str,
    race: str,
    race_type: Optional[str],
    status: str,
    location: str,
    item: str,
    quest_id: int,
    step: Optional[int],
):
    quest_full_id = normalizeQuest(player_class=player_class, race=race, race_type=race_type, quest_id=quest_id)
    quest_id_with_step = quest_full_id + step
    # loads templates dir
    file_loader = FileSystemLoader("app/templates")
    env = Environment(loader=file_loader)
    template = env.get_template("quest.txt")
    output = template.render(
        quest_id={quest_id}, status={status}, location={location}, item={item}, race_type={race_type}, step={step}
    )
    return {"message": output}
