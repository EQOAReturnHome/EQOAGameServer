function event_say()
diagOptions = {}
    npcDialogue = "How fortunate you are to be upon the wings of an unpredictable destiny! Hopefully, the bards will too speak of your legends!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end