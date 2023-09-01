function event_say()
diagOptions = {}
    npcDialogue = "Have you ever been lost in your thoughts? Lately, I catch myself wondering where the day has gone when I suddenly snap back from a stupor."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end