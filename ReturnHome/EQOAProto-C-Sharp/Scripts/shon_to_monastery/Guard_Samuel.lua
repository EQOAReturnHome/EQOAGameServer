function event_say()
diagOptions = {}
    npcDialogue = "Welcome to Chiasta. Please respect the wishes of the monks and citizens."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end