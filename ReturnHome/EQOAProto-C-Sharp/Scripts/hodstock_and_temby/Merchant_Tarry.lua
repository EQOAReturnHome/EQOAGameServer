function  event_say(choice)
diagOptions = {}
    npcDialogue = "I might have a few supplies here."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end