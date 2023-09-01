function  event_say(choice)
diagOptions = {}
    npcDialogue = "Care to be bound to this location, playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end