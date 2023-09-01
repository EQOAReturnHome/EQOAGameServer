function  event_say(choice)
diagOptions = {}
    npcDialogue = "Perhaps you'd like me to bind yer spirit to this location, playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end