from fastapi import FastAPI
from app.routes import base
from app.routes.v1 import quests
from app.routes.v1 import npc_editor
from fastapi.middleware.cors import CORSMiddleware

from app.core.config import settings


def get_application():
    _app = FastAPI(title=settings.PROJECT_NAME)

    _app.add_middleware(
        CORSMiddleware,
        allow_origins=["*"],
        allow_credentials=True,
        allow_methods=["*"],
        allow_headers=["*"],
    )

    return _app


app = get_application()

app.include_router(base.router)
app.include_router(quests.router)
app.include_router(npc_editor.router)
