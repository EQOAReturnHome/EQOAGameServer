function  event_say(choice)
diagOptions = {}
    npcDialogue = "A pleasure to make your acquaintance playerName, but I am pressed for time. Please see yourself out."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end