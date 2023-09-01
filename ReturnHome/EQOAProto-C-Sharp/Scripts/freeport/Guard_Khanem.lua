function  event_say(choice)
diagOptions = {}
    npcDialogue = "As you were, citizen."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end