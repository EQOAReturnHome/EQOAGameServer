function  event_say(choice)
diagOptions = {}
    npcDialogue = "Whatever it is you want, I highly doubt I can help you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end