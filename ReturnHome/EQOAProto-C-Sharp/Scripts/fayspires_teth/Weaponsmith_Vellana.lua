function event_say()
diagOptions = {}
    npcDialogue = "Many weapons pass through here but none are more potent than the ones you make yourself. Would you like to learn how playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end