function event_say()
diagOptions = {}
    npcDialogue = "When you learn of secrets you don't spread them, you exploit them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end