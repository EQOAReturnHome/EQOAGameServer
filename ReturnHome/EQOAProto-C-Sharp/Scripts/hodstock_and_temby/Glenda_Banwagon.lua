function  event_say(choice)
diagOptions = {}
    npcDialogue = "Need a drink?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end