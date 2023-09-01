function  event_say(choice)
diagOptions = {}
    npcDialogue = "What can I do for you playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end