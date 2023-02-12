from fastapi import APIRouter
from typing import Optional
from jinja2 import Environment, FileSystemLoader
from logging import Logger

log = Logger("logger")

router = APIRouter()


@router.get("/npc_editor")
async def generate(
    player_class: str,
    race: str,
    race_type: Optional[str],
    status: str,
    location: str,
    npc: str,
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
        quest_id={quest_id}, status={status}, location={location}, npc={npc}, race_type={race_type}, step={step}
    )
    return {"message": output}


def normalizeQuest(player_class: str, race: str, race_type: str, quest_id: int):
    # takes in specific values, returns number combo for quest object
    player_classes = {
        "warrior": "0",
        "ranger": "1",
        "paladin": "2",
        "shadowknight": "3",
        "monk": "4",
        "bard": "5",
        "rogue": "6",
        "druid": "7",
        "shaman": "8",
        "cleric": "9",
        "magician": "10",
        "necromancer": "11",
        "enchanter": "12",
        "wizard": "13",
        "alchemist": "14",
    }

    races = {
        "human": "0",
        "elf": "1",
        "dark_elf": "2",
        "gnome": "3",
        "dwarf": "4",
        "troll": "5",
        "barbarian": "6",
        "halfling": "7",
        "erudite": "8",
        "ogre": "9",
    }

    race_origin = {"other": "0", "eastern": "1", "western": "2"}

    class_id = player_classes.get(player_class)
    race_id = races.get(race)
    race_origin_id = race_origin.get(race_type)

    if not class_id:
        log.error("{'message': 'No Class input, could not generate class_id'}")
        return

    if not race_id:
        log.error("{'message': 'No race input, could not generate race_id'}")
        return

    if not race_origin_id:
        log.error("{'message': 'No race origin input, could not generate race_origin'}")
        return

    if not quest_id:
        log.error("{'message': 'No quest number, could not generate quest_id'}")
        return

    return class_id + race_id + race_origin_id + str(quest_id)
