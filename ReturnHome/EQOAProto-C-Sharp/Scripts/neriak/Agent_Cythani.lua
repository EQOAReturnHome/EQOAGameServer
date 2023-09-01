function  event_say(choice)
diagOptions = {}
    npcDialogue = "Leave us in peace, we have planning to do."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end