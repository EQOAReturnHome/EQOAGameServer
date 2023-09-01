function event_say()
diagOptions = {}
    npcDialogue = "Here at The Fayspires, we preserve the legacy of elves on Tunaria!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end