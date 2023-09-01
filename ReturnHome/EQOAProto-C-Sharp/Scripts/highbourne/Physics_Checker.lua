function event_say()
diagOptions = {}
    npcDialogue = "These calculations must be precise."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end