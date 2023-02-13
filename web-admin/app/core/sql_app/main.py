from typing import List

import uvicorn
from sqlalchemy.orm import Session
from fastapi import Depends, FastAPI, HTTPException

import models, schemas, crud
from database import engine, SessionLocal

models.Base.metadata.create_all(bind=engine)

app = FastAPI()

# Dependency


def get_db():
    db = None
    try:
        db = SessionLocal()
        yield db
    finally:
        db.close()


@app.post("/npc", response_model=schemas.NPCInfo)
def create_npc(npc: schemas.CreateNPC, db: Session = Depends(get_db)):
    new_npc = crud.get_npc_by_name(db, npc_name=npc.npc_name)
    if new_npc:
        raise HTTPException(status_code=400, detail="NPC already exists")
    return crud.create_npc(db=db, npc=npc)


if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8081)
