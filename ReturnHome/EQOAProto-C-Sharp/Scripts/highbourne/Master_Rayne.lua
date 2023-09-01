function event_say()
diagOptions = {}
    npcDialogue = "The view is inspiring. It reminds me of just how small we all are in this world."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end