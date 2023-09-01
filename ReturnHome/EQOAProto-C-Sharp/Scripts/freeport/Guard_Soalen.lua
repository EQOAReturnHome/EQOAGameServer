function  event_say(choice)
diagOptions = {}
    npcDialogue = "This isn't a great city for tourists, playerName. Best you head out before something unfortunate happens."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end