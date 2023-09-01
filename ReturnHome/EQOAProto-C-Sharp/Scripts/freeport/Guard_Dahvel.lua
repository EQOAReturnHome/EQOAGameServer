function  event_say(choice)
diagOptions = {}
    npcDialogue = "Crooked merchants in Freeport? I don't know what you're talking about."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end