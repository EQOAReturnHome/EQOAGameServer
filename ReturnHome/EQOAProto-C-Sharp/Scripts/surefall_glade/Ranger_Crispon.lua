function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to Surefall Glade, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end