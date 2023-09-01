function  event_say(choice)
diagOptions = {}
    npcDialogue = "You stand in the House of Do'Vexis, playerName. I hope you're not here to cause trouble."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end