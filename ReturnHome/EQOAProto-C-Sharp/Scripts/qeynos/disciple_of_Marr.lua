function  event_say(choice)
diagOptions = {}
    npcDialogue = "Peace be with you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end