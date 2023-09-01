function event_say()
diagOptions = {}
    npcDialogue = "Hello there. I'm afraid time waits for nothing, especially not you or me. Return to your studies and exercises."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end