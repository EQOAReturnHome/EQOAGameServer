function event_say()
diagOptions = {}
    npcDialogue = "I didn't realize the sheer amount of review involved when I accepted this position. One would expect these researchers and historians to be accurate…so many errors and duplicated errors."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end