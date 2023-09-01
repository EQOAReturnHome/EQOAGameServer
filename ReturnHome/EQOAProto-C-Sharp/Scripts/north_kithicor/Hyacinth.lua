function event_say()
diagOptions = {}
    npcDialogue = "I have a particular set of skills. My poisons are strong, my strikes swift and clean. They are available…for a price."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end