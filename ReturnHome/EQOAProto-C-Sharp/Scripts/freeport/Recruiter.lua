function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm afraid I don't have any business for you, playerName. Perhaps some other time?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end