function  event_say(choice)
diagOptions = {}
    npcDialogue = "playerName, may I bind your spirit here?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end