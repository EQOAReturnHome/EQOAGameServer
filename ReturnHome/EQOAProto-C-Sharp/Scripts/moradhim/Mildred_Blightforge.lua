function  event_say(choice)
diagOptions = {}
    npcDialogue = "The trophy of a champion may come in handy on your journeys, but remember, yer true worth is what's inside yer heart, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end