function  event_say(choice)
diagOptions = {}
    npcDialogue = "*squeak*"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end