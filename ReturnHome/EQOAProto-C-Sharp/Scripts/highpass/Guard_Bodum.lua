function  event_say(choice)
diagOptions = {}
    npcDialogue = "Stop looking at me, peasant."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end