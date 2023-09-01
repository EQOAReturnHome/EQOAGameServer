function  event_say(choice)
diagOptions = {}
    npcDialogue = "Leave my tent immediately, worm! Can you not see we are discussing battle tactics?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end