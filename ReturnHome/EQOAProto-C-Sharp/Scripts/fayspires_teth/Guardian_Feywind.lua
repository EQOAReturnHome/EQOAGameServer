function event_say()
diagOptions = {}
    npcDialogue = "Welcome to Paragon Keep. Here we train elven paladins to become guardians, and great keepers of the peace."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end