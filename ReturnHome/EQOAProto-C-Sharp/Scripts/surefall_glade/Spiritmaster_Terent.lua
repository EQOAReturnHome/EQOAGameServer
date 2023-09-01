function  event_say(choice)
diagOptions = {}
    npcDialogue = "Hello playerName. Would you like me to bind you here?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end