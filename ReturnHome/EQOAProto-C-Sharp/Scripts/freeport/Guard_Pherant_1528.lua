function  event_say(choice)
diagOptions = {}
    npcDialogue = "If we are ever attacked by way of the ocean, let me be the first to defend our great city!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end