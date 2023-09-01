function event_say()
diagOptions = {}
    npcDialogue = "I am quite intrigued that these primitive orcs have discovered a metal with magical properties that I have not yet seen in all of my research. Excuse me now playerName, I must find a way to acquire this metal."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end