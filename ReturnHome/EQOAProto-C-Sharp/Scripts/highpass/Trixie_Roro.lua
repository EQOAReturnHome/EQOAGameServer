function  event_say(choice)
diagOptions = {}
    npcDialogue = "This keep may feel safe but don't let your guard down.  A pickpocket could rob you blind any moment."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end