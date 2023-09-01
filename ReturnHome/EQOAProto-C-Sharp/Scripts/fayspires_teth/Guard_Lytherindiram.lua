function event_say()
diagOptions = {}
    npcDialogue = "You needn't worry, you are quite safe here, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end