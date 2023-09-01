function event_say()
diagOptions = {}
    npcDialogue = "Have you heard Egrain's latest epic? It is full of twists and turns, deaths and glorious victory."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end