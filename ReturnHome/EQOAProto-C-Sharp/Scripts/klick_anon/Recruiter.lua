function  event_say(choice)
diagOptions = {}
    npcDialogue = "Someday playerName, you too must choose between the path of evil, and the path of good."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end