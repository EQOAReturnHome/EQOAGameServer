function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm watching you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end