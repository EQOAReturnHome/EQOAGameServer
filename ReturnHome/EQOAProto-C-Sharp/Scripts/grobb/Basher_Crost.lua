function  event_say(choice)
diagOptions = {}
    npcDialogue = "Warlord Jurglash has no time to waste. Better you stay out of his way. Don't even go near him playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end