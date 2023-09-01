function event_say()
diagOptions = {}
    npcDialogue = "Close your eyes and open your mind. Listen to the world around you. Prexus speaks to all but only the worthy can discern the words."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end