function  event_say(choice)
diagOptions = {}
    npcDialogue = "Go about your business, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end