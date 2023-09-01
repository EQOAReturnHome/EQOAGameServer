function  event_say(choice)
diagOptions = {}
    npcDialogue = "I will bind you to this location, if you ask me to playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end