function  event_say(choice)
diagOptions = {}
    npcDialogue = "Would you like to purchase Boon of Hagley?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end