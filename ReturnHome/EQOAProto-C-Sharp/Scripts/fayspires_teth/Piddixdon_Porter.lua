function event_say()
diagOptions = {}
    npcDialogue = "I am working on a method to port adventurers to other places. Perhaps I can help you in time."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end