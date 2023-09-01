function  event_say(choice)
diagOptions = {}
    npcDialogue = "You are entering the proud city of Neriak, playerName. Try not to cause us any issues."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end