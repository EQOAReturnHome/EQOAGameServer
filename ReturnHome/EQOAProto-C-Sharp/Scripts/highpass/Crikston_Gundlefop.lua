function  event_say(choice)
diagOptions = {}
    npcDialogue = "I don't know you, and I don't care to."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end