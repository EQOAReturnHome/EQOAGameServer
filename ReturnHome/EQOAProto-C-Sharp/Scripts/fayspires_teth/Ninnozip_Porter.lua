function event_say()
diagOptions = {}
    npcDialogue = "I am working on a method to port you to other places. Check back with me later."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end