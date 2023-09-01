function  event_say(choice)
diagOptions = {}
    npcDialogue = "Purchase the Boon of Hagley?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end