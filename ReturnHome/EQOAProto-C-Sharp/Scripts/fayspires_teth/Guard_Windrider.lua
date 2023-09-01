function event_say()
diagOptions = {}
    npcDialogue = "Many blessings of Tunare to you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end