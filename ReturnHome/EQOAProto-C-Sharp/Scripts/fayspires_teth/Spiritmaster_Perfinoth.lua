function event_say()
diagOptions = {}
    npcDialogue = "If it is best for you, I have the power to bind your soul to this place. May I do this playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end