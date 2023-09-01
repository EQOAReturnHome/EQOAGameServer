function  event_say(choice)
diagOptions = {}
    npcDialogue = "I am not just the maid, playerName. I am also the queen's royal guardian. Fear not, as I am squarely focused on her safety at all times."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end