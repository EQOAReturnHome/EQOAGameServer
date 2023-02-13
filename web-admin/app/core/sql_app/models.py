from sqlalchemy import Column, Integer, String
from app.core.sql_app.database import Base


class NPCInfo(Base):
    __tablename__ = "npcs"

    id = Column(Integer, unique=True, primary_key=True, index=True)
    npc_name = Column(String, unique=True)
    zone = Column(Integer)
