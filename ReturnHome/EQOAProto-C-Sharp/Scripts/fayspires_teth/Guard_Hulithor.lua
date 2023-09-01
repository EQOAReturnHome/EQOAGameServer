function event_say()
diagOptions = {}
    npcDialogue = "I have served the minister for many years. He has been acting strange lately. He pours through old texts, and mutters foreign tongues that I do not recognize."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end