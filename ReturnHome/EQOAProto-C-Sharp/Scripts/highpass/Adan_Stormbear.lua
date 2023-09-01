function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm quite busy.  Please leave me be."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end