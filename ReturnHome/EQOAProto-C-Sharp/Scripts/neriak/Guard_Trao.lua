function  event_say(choice)
diagOptions = {}
    npcDialogue = "Look at me again and my blade shall greet your throat."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end