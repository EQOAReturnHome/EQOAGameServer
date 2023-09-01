function event_say()
diagOptions = {}
    npcDialogue = "No matter how far I travel, I always long to get back to this place. I believe the lake contains healing properties, at least for us elves."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end