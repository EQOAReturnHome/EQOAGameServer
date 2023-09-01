function event_say()
diagOptions = {}
    npcDialogue = "The senate keeps me busy day and night. It is starting to get out of hand…I may have to take on an adventurer to complete some of these tasks."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end