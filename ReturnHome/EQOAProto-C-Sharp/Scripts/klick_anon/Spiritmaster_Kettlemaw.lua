function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can bind your spirit here playerName, if you like."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end