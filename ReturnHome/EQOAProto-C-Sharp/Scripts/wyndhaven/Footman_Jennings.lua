function  event_say(choice)
diagOptions = {}
    npcDialogue = "The alpha gnoll is dressed in ornate clothing. You can tell he is in charge of the others, but to be honest, he looked kind of silly dressed like that."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end