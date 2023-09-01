function  event_say(choice)
diagOptions = {}
    npcDialogue = "What would you like?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end