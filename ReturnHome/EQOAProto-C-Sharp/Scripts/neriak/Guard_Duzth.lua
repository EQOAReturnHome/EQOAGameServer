function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to Neriak, playerName. You would do well not to stir up any mischief."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end