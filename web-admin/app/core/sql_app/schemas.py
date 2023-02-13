from typing import List
from pydantic import BaseModel


class NPCInfoBase(BaseModel):
    npc_name: str


class CreateNPC(NPCInfoBase):
    zone: int

class NPCInfo(NPCInfoBase):
    id: int

    class Config:
        orm_mode = True
