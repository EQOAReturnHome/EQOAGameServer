function  event_say(choice)
diagOptions = {}
    npcDialogue = "All I want is a cold grog and to talk to that sweet Barkeep Maya."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end