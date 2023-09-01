function event_say()
diagOptions = {}
    npcDialogue = "Welcome to the Church of Tunare. In this temple our holy clerics learn to call forth the healing powers of the Goddess."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end