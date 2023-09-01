function  event_say(choice)
diagOptions = {}
    npcDialogue = "playerName, you must practice everyday if you wish ta keep yer wits and reflexes sharp!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end