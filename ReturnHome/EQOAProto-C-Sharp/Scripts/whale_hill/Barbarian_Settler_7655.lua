function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to Binshore, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end