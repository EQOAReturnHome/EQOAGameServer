function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'd be happy to bind you here playerName, if that would help at all."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end