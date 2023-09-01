function event_say()
diagOptions = {}
    npcDialogue = "I'm working on a new protective helmet design for the dock workers based on beetle carapaces. I am going to call it the Hard Hat."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end