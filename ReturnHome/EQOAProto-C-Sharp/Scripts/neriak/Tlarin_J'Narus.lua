function  event_say(choice)
diagOptions = {}
    npcDialogue = "Do you need something playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end