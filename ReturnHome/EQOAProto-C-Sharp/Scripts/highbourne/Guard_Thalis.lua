function event_say()
diagOptions = {}
    npcDialogue = "The worker's quarters is a quiet place. I like to keep it that way."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end