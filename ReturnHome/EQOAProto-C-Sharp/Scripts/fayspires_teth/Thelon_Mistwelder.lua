function event_say()
diagOptions = {}
    npcDialogue = "The young druids have been thinning in numbers as of late. I am concerned that the duties that the druids maintain will one day become impossible to uphold. We will need to find ways to compensate for this, or nature may be also lost to the twilight."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end