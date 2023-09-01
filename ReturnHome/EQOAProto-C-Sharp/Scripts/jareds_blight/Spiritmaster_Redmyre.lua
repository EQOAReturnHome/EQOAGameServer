function  event_say(choice)
diagOptions = {}
    npcDialogue = "Of course I would be happy to bind you here, playername."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end