function  event_say(choice)
diagOptions = {}
    npcDialogue = "I've got my eye on you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end