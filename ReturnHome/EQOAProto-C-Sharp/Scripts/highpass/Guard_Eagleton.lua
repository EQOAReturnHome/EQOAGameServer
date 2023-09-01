function  event_say(choice)
diagOptions = {}
    npcDialogue = "Move along, citizen."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end