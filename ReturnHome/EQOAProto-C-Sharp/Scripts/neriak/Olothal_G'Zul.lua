function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm not sure if we'll ever be rid of those damned rats."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end