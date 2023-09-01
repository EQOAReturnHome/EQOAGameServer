function  event_say(choice)
diagOptions = {}
    npcDialogue = "Leave me alone. Can't you see I'm busy playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end