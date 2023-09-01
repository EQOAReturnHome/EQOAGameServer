function event_say()
diagOptions = {}
    npcDialogue = "Welcome to the elven city of Tethelin. Please mind your step while walking amongst the treetops."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end