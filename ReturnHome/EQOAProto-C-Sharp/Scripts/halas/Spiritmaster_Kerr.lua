function  event_say(choice)
diagOptions = {}
    npcDialogue = "Death comes for us all, playerName. Best be ready for it. Would you like me to bind your spirit to this location?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end