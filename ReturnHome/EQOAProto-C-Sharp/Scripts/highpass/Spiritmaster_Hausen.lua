function  event_say(choice)
diagOptions = {}
    npcDialogue = "Would you like me to bound your spirit to this location, child?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end