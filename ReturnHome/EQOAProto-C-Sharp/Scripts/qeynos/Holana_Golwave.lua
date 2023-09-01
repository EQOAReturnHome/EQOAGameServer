function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to the Qeynos Theatre. Perhaps you'll show us your talents on the stage?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end