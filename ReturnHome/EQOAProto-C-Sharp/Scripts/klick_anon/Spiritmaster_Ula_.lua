function  event_say(choice)
diagOptions = {}
    npcDialogue = "I would be happy to bind your spirit here. Would you like that playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end