function  event_say(choice)
diagOptions = {}
    npcDialogue = "Would you like me to teach you how to fish?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end