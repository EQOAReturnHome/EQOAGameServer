function  event_say(choice)
diagOptions = {}
    npcDialogue = "Stand back dear playerName, this isn't for the faint of heart. "
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end