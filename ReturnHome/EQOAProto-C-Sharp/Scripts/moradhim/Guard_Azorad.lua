function  event_say(choice)
diagOptions = {}
    npcDialogue = "Some of the city leaders are havin' a meetin' at the moment. Only enter if ye have official business, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end