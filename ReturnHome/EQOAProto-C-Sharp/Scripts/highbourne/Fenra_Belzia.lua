function event_say()
diagOptions = {}
    npcDialogue = "I am innocent I tell you!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end