function event_say()
diagOptions = {}
    npcDialogue = "These erudite craftsman are amazing…but everyone knows you need at least one gnome engineer to make sure your plans are right."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end