function  event_say(choice)
diagOptions = {}
    npcDialogue = "Sorry playerName, but I am extremely busy at the moment."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end