function  event_say(choice)
diagOptions = {}
    npcDialogue = "Consider binding your spirit here, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end