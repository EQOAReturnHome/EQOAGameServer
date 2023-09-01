function event_say()
diagOptions = {}
    npcDialogue = "Welcome to Bastable. Mind your manners and we'll get along just fine."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end