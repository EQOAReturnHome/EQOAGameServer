function  event_say(choice)
diagOptions = {}
    npcDialogue = "Fishing can be quite relaxing, and rewarding. Care to learn how to fish?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end