function  event_say(choice)
diagOptions = {}
    npcDialogue = "Leave me alone. Can't you see I'm busy?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end