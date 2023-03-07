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
def get_item(item: schemas.GetItem, db: Session = Depends(get_db)):
    new_item = crud.get_item_by_name(db, itemname=item.itemname)
    if new_item is None:
        raise HTTPException(status_code=400, detail="No item Found")
    return crud.get_item_by_name(db, itemname=item.itemname)

@router.post("/update_item", response_model=schemas.ItemInfo)
def update_item(item: schemas.UpdateItem, db: Session = Depends(get_db)):
    existing_item = crud.get_item_by_name(db, itemname=item.itemname)
    if existing_item is None:
        raise HTTPException(status_code=400, detail="No item Found")
    return crud.update_item(db, item=item)
