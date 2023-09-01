function event_say()
diagOptions = {}
    npcDialogue = "Would you like to purchase Boon of Hagley?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end