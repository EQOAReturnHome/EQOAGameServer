function event_say()
diagOptions = {}
    npcDialogue = "Stay a while and listen. I've traveled much, gathered facts and superstitious tales alike. Enough to make some of the Erudites envious."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end