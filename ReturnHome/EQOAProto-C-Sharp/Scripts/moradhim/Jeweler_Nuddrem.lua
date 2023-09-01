function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can teach ye a thing or two about jewelcrafting, if ye'd like."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end